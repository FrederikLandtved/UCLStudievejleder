using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Generic;
using DatabaseAccess.Institution.Models;
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

        public List<InstitutionModel> GetAllInstitutions()
        {
            List<InstitutionModel> institutions = new List<InstitutionModel>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[Institution]");

            while (reader.Read())
            {
                institutions.Add(new InstitutionModel
                {
                    InstitutionId = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return institutions;

        }

        public List<InstitutionModel> GetInstitutionsByUserId(int userId)
        {
            List<InstitutionModel> institutions = new List<InstitutionModel>();

            SqlDataReader reader = _genericSql.ExecuteSproc("GetInstitutionsFromUserId", new List<ParameterModel> { new ParameterModel { Parameter = "@UserId", Value = userId.ToString()} });

            while (reader.Read())
                institutions.Add(new InstitutionModel { InstitutionId = reader.GetInt32(0), Name = reader.GetString(1) });

            return institutions;
        }

        public InstitutionModel GetInstitution(int institutionId)
        {
            InstitutionModel institution = new InstitutionModel();

            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[Institution] WHERE InstitutionId = " + institutionId);

            while (reader.Read())
            {
                institution.InstitutionId = reader.GetInt32(0);
                institution.Name = reader.GetString(1);
            }

            return institution;
        }

        public void LinkUserToInstitution(int userId, int institutionId)
        {
            string query = "INSERT INTO dbo.[UserHasInstitution] (UserId, InstitutionId) VALUES (@userId, @institutionId)";


            List<ParameterModel> inserts = new List<ParameterModel>
            {
                new ParameterModel { Parameter = "@userId", Value = userId.ToString() },
                new ParameterModel { Parameter = "@institutionId", Value = institutionId.ToString() }

            };

            _genericSql.Insert(query, inserts);
        }
    }
}
