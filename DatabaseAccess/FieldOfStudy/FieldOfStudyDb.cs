using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;

namespace DatabaseAccess.FieldOfStudy
{
    public class FieldOfStudyDb
    {
        string connectionString = "Server=tcp:ucldataserver.database.windows.net,1433;Initial Catalog=UCLDataPROD;Persist Security Info=False;User ID=azureuser;Password=Stefan$ebastianJacobMia;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        private readonly GenericSql _genericSql;
        public FieldOfStudyDb()
        {
            _genericSql = new GenericSql();
        }

        public List<FieldOfStudy> GetAllFieldsOfStudy()
        {
            List<FieldOfStudy> fieldOfStudies = new List<FieldOfStudy>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[FieldOfStudy]");

            while (reader.Read())
            {
                fieldOfStudies.Add(new FieldOfStudy
                {
                    FieldOfStudyId = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return fieldOfStudies;

        }
        public void LinkUserToFieldOfStudy(int userId, int fieldOfStudyId)
        {
            string query = "INSERT INTO dbo.[UserHasFieldOfStudy] (UserId, FieldOfStudyId) VALUES (@userId, @fieldOfStudyId)";
           
            List<InsertModel> inserts = new List<InsertModel>
            {
                new InsertModel { Parameter = "@userId", Value = userId.ToString() },
                new InsertModel { Parameter = "@fieldOfStudyId", Value = fieldOfStudyId.ToString() }

            };

            _genericSql.Insert(query, inserts);
        }

        public class FieldOfStudy
        {
            public int FieldOfStudyId { get; set; }
            public string Name { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
