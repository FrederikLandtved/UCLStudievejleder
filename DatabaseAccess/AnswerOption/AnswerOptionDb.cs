using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;

namespace DatabaseAccess.Question
{
    public class AnswerOptionDb
    {
        private readonly GenericSql _genericSql;
        public AnswerOptionDb()
        {
            _genericSql = new GenericSql();
        }

        public List<AnswerOption> GetAnswerOptionsByQuestionId(int questionId)
        {
            List<AnswerOption> answerOptions = new List<AnswerOption>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[AnswerOption] WHERE QuestionId = " + questionId);

            while (reader.Read())
            {
                answerOptions.Add(new AnswerOption
                {
                    AnswerOptionId = reader.GetInt32(0),
                    QuestionId = reader.GetInt32(1),
                    AnswerOptionString = reader.GetString(2),
                });
            }

            return answerOptions;
        }

        public class AnswerOption
        {
            public int AnswerOptionId { get; set; }
            public int QuestionId { get; set; }
            public string AnswerOptionString { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
