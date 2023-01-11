using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;
using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Institution.Models;
using DatabaseAccess.Institution;
using Microsoft.Extensions.Caching.Memory;

namespace DatabaseAccess.FieldOfStudy
{
    public class FieldOfStudyDb
    {
        private readonly GenericSql _genericSql;
        private readonly IMemoryCache _memoryCache;

        public FieldOfStudyDb(IMemoryCache memoryCache, GenericSql genericSql)
        {
            _genericSql = genericSql;
            _memoryCache = memoryCache;
        }

        public List<FieldOfStudyModel> GetAllFieldsOfStudy()
        {
            List<FieldOfStudyModel> fieldOfStudies = new List<FieldOfStudyModel>();
            var cachedFields = _memoryCache.Get<List<FieldOfStudyModel>>("AllFieldsOfStudy");
            if (cachedFields != null)
                return cachedFields;

            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[FieldOfStudy]");

            while (reader.Read())
            {
                fieldOfStudies.Add(new FieldOfStudyModel
                {
                    FieldOfStudyId = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            _memoryCache.Set("AllFieldsOfStudy", fieldOfStudies, TimeSpan.FromDays(1));
            return fieldOfStudies;

        }

        public List<FieldOfStudyModel> GetFieldsOfStudyByUserId(int userId)
        {
            // We create a new list to store all the FieldsOfStudies connected to a user
            List<FieldOfStudyModel> fieldOfStudies = new List<FieldOfStudyModel>();

            // We execute the Stored Procedure "sp_GetFieldsOfStudyFromUserId"
            // And pass the userId as a parameter
            SqlDataReader reader = _genericSql.ExecuteSproc("sp_GetFieldsOfStudyFromUserId", new List<ParameterModel> { new ParameterModel { Parameter = "@UserId", Value = userId.ToString() } });

            // Foreach field of study that we get back from the stored procedure, we add it to the list that we created first.
            while (reader.Read())
                fieldOfStudies.Add(new FieldOfStudyModel { FieldOfStudyId = reader.GetInt32(0), Name = reader.GetString(1), IsSelected = true });
            
            // We return the list that we created first.
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
