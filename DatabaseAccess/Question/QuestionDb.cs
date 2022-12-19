using DatabaseAccess.Generic;
using DatabaseAccess.Institution;
using DatabaseAccess.Institution.Models;
using DatabaseAccess.User;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
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

        public void CreateQuestionAnswer(int questionId, int answerOptionId, int formularId)
        {
            string query = "INSERT INTO dbo.[QuestionAnswer] (QuestionId, AnswerOptionId, FormularId) VALUES (@questionId, @answerOptionId, @formularId)";


            List<ParameterModel> inserts = new List<ParameterModel>
            {
                new ParameterModel { Parameter = "@questionId", Value = questionId.ToString() },
                new ParameterModel { Parameter = "@answerOptionId", Value = answerOptionId.ToString() },
                new ParameterModel { Parameter = "@formularId", Value = formularId.ToString() }

            };

            _genericSql.Insert(query, inserts);
        }


        public class Question
        {
            public int QuestionId { get; set; }
            public string QuestionString { get; set; }
            public int QuestionTypeId { get; set; }
            public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

            public int SingleChosenOption { get; set; }
            public string DropdownChosenOption { get; set; }
        }
    }
}
