using DatabaseAccess.Generic;
using DatabaseAccess.Institution;
using DatabaseAccess.Institution.Models;
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
        private readonly IMemoryCache _memoryCache;

        public QuestionDb(IMemoryCache memoryCache)
        {
            _genericSql = new GenericSql();
            _answerOptionDb = new AnswerOptionDb();
            _memoryCache = memoryCache;
        }

        public List<Question> GetAllQuestionsWithAnswers()
        {
            List<Question> questions = new List<Question>();
            var cachedQuestions = _memoryCache.Get<List<Question>>("AllQuestionsWithAnswers");
            if (cachedQuestions != null)
                return cachedQuestions;

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

            _memoryCache.Set("AllQuestionsWithAnswers", questions, TimeSpan.FromMinutes(5));
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
