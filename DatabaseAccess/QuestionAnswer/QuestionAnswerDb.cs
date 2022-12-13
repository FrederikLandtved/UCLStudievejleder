using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.FormularDbAccess.FormularDbAccess;

namespace DatabaseAccess.QuestionAnswer
{
    public class QuestionAnswerDb
    {
        private readonly GenericSql _genericSql;

        public QuestionAnswerDb()
        {
            _genericSql = new GenericSql();
        }

        public List<QuestionAnswerModel> GetQuestionAnswersForFormular(int formularId)
        {
            List<QuestionAnswerModel> formulars = new List<QuestionAnswerModel>();

            SqlDataReader reader = _genericSql.Select("SELECT QuestionId, AnswerOptionId, FormularId FROM [dbo].[QuestionAnswer] WHERE FormularId = " + formularId);

            while (reader.Read())
            {
                formulars.Add(new QuestionAnswerModel
                {
                    QuestionId = reader.GetInt32(0),
                    AnswerOptionId = reader.GetInt32(1),
                    FormularId = reader.GetInt32(2)
                });
            }

            return formulars;
        }

        public class QuestionAnswerModel
        {
            public int QuestionAnswerId { get; set; }
            public int QuestionId { get; set; }
            public int AnswerOptionId { get; set; }
            public int FormularId { get; set; }
        }
    }
}
