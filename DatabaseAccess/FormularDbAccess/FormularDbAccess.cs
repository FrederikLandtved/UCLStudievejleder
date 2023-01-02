using DatabaseAccess.Generic;
using DatabaseAccess.Question;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using static DatabaseAccess.Generic.GenericSql;
using static DatabaseAccess.Question.AnswerOptionDb;
using static DatabaseAccess.Question.QuestionDb;

namespace DatabaseAccess.FormularDbAccess
{
    public class FormularDbAccess
    {
        private readonly GenericSql _genericSql;

        public FormularDbAccess(IMemoryCache memoryCache, GenericSql genericSql)
        {
            _genericSql = genericSql;
        }

        public int CreateNewFormular(int userId, DateTime dateSubmitted)
        {
            int newFormularId = _genericSql.InsertRowAndReturnId("sp_CreateNewFormular", new List<ParameterModel> 
            { 
                new ParameterModel { Parameter = "@UserId", Value = userId.ToString() },
                new ParameterModel { Parameter = "@DateSubmitted", Value = dateSubmitted.ToString("yyyy-MM-dd") }
            }, "@FormularId");

            return newFormularId;
        }

        public List<FormularModel> GetFormularsForUser(int userId)
        {
            List<FormularModel> formulars = new List<FormularModel>();

            SqlDataReader reader = _genericSql.Select("SELECT FormularId, UserId, DateSubmitted FROM [dbo].[Formular] WHERE UserId = " + userId);

            while (reader.Read())
            {
                formulars.Add(new FormularModel
                {
                    FormularId = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    DateSubmitted = reader.GetDateTime(2),
                });
            }

            return formulars;
        }

        public class FormularModel
        {
            public int FormularId { get; set; }
            public int UserId { get; set; }
            public DateTime DateSubmitted { get; set; }
        }
    }
}
