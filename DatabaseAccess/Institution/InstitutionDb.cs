using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Generic;
using DatabaseAccess.Institution.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using static DatabaseAccess.Generic.GenericSql;

namespace DatabaseAccess.Institution
{
    public class InstitutionDb
    {
        private readonly GenericSql _genericSql;
        private readonly IMemoryCache _memoryCache;
        public InstitutionDb(IMemoryCache memoryCache, GenericSql genericSql)
        {
            _genericSql = genericSql;
            _memoryCache = memoryCache;
        }

        public List<InstitutionModel> GetAllInstitutions()
        {
            List<InstitutionModel> institutions = new List<InstitutionModel>();
            var cachedInstitutions = _memoryCache.Get<List<InstitutionModel>>("AllInstitutions");
            if (cachedInstitutions != null)
                return cachedInstitutions;

            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[Institution]");

            while (reader.Read())
            {
                institutions.Add(new InstitutionModel
                {
                    InstitutionId = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            _memoryCache.Set("AllInstitutions", institutions, TimeSpan.FromDays(1));
            return institutions;

        }

        public List<InstitutionModel> GetInstitutionsByUserId(int userId)
        {
            // We create a new list to store all the Institutions that are connected to a user
            List<InstitutionModel> institutions = new List<InstitutionModel>();

            // We execute the stored procedure "GetInstitutionsFromUserId"
            // and pass the userId as parameter
            SqlDataReader reader = _genericSql.ExecuteSproc("GetInstitutionsFromUserId", new List<ParameterModel> { new ParameterModel { Parameter = "@UserId", Value = userId.ToString()} });

            // For each Institution we get back from the Stored Procedure, we add it to the list that we created first
            while (reader.Read())
                institutions.Add(new InstitutionModel { InstitutionId = reader.GetInt32(0), Name = reader.GetString(1), IsSelected = true });

            // We return the list that we created first.
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
