using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;

namespace DatabaseAccess.Institution
{
    public class InstitutionDb
    {
        string connectionString = "Server=tcp:ucldataserver.database.windows.net,1433;Initial Catalog=UCLDataPROD;Persist Security Info=False;User ID=azureuser;Password=Stefan$ebastianJacobMia;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        private readonly GenericSql _genericSql;
        public InstitutionDb()
        {
            _genericSql = new GenericSql();
        }

        public List<Institution> GetAllInstitutions()
        {
            List<Institution> institutions = new List<Institution>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[Institution]");

            while (reader.Read())
            {
                institutions.Add(new Institution
                {
                    InstitutionId = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return institutions;

        }
        public void LinkUserToInstitution(int userId, int institutionId)
        {
            string query = "INSERT INTO dbo.[UserHasInstitution] (UserId, InstitutionId) VALUES (@userId, @institutionId)";


            List<InsertModel> inserts = new List<InsertModel>
            {
                new InsertModel { Parameter = "@userId", Value = userId.ToString() },
                new InsertModel { Parameter = "@institutionId", Value = institutionId.ToString() }

            };

            _genericSql.Insert(query, inserts);
        }
    }

    public class Institution : UCLListItem
    {
        public int InstitutionId { get; set; }
        public string Name { get; set; }
    }


    public class UCLListItem
    {
        public bool IsSelected { get; set; }
    }
}
