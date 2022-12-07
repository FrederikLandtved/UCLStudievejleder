using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;
using static DatabaseAccess.Question.AnswerOptionDb;

namespace DatabaseAccess.Question
{
    public class QuestionDb
    {
        private readonly GenericSql _genericSql;
        private readonly AnswerOptionDb _answerOptionDb;
        public QuestionDb()
        {
            _genericSql = new GenericSql();
            _answerOptionDb = new AnswerOptionDb();
        }

        public List<Question> GetAllQuestionsWithAnswers()
        {
            List<Question> questions = new List<Question>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[Question]");

            while (reader.Read())
            {
                questions.Add(new Question
                {
                    QuestionId = reader.GetInt32(0),
                    QuestionString = reader.GetString(1),
                    QuestionTypeId = reader.GetInt32(2),
                    AnswerOptions = _answerOptionDb.GetAnswerOptionsByQuestionId(reader.GetInt32(0))
                });
            }

            return questions;
        }


        public class Question
        {
            public int QuestionId { get; set; }
            public string QuestionString { get; set; }
            public int QuestionTypeId { get; set; }
            public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
        }
    }
}
