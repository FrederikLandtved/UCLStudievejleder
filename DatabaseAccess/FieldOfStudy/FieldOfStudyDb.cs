using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;
using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Institution.Models;
using DatabaseAccess.Institution;

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

        public List<FieldOfStudyModel> GetAllFieldsOfStudy()
        {
            List<FieldOfStudyModel> fieldOfStudies = new List<FieldOfStudyModel>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[FieldOfStudy]");

            while (reader.Read())
            {
                fieldOfStudies.Add(new FieldOfStudyModel
                {
                    FieldOfStudyId = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return fieldOfStudies;

        }

        public List<FieldOfStudyModel> GetFieldsOfStudyByUserId(int userId)
        {
            List<FieldOfStudyModel> fieldOfStudies = new List<FieldOfStudyModel>();

            SqlDataReader reader = _genericSql.ExecuteSproc("sp_GetFieldsOfStudyFromUserId", new List<ParameterModel> { new ParameterModel { Parameter = "@UserId", Value = userId.ToString() } });

            while (reader.Read())
                fieldOfStudies.Add(new FieldOfStudyModel { FieldOfStudyId = reader.GetInt32(0), Name = reader.GetString(1) });

            return fieldOfStudies;
        }

        public FieldOfStudyModel GetFieldOfStudy(int fieldOfStudyId)
        {
            FieldOfStudyModel fieldOfStudy = new FieldOfStudyModel();

            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[FieldOfStudy] WHERE FieldOfStudyId = " + fieldOfStudyId);

            while (reader.Read())
            {
                fieldOfStudy.FieldOfStudyId = reader.GetInt32(0);
                fieldOfStudy.Name = reader.GetString(1);
            }

            return fieldOfStudy;
        }

        public void LinkUserToFieldOfStudy(int userId, int fieldOfStudyId)
        {
            string query = "INSERT INTO dbo.[UserHasFieldOfStudy] (UserId, FieldOfStudyId) VALUES (@userId, @fieldOfStudyId)";
           
            List<ParameterModel> inserts = new List<ParameterModel>
            {
                new ParameterModel { Parameter = "@userId", Value = userId.ToString() },
                new ParameterModel { Parameter = "@fieldOfStudyId", Value = fieldOfStudyId.ToString() }

            };

            _genericSql.Insert(query, inserts);
        }
    }
}
