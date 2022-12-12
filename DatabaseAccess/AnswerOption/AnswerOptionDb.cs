using DatabaseAccess.FieldOfStudy.Models;
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

        public string GetAnswerOptionString(int answerOptionId)
        {
            string answerOption = "";
            SqlDataReader reader = _genericSql.Select("SELECT AnswerOptionString FROM [dbo].[AnswerOption] WHERE AnswerOptionId = " + answerOptionId);

            while (reader.Read())
            {
                answerOption = reader.GetString(0);
            }

            return answerOption;
        }

        public List<FormularAnswers> GetFormularAnswers(int userId)
        {
            List<FormularAnswers> formAnswers = new List<FormularAnswers>();

            SqlDataReader reader = _genericSql.ExecuteSproc("sp_GetFormularAnswersFromUserId", new List<ParameterModel> { new ParameterModel { Parameter = "@UserId", Value = userId.ToString() } });

            while (reader.Read())
                formAnswers.Add(
                    new FormularAnswers
                    { 
                        UserId = reader.GetInt32(0),
                        FormularId = reader.GetInt32(1),
                        QuestionId = reader.GetInt32(2),
                        QuestionString = reader.GetString(3),
                        QuestionAnswer = reader.GetString(4),
                        DateSubmitted = reader.GetDateTime(5)
                    });

            return formAnswers;
        }

        public class AnswerOption
        {
            public int AnswerOptionId { get; set; }
            public int QuestionId { get; set; }
            public string AnswerOptionString { get; set; }
            public bool IsSelected { get; set; }
        }

        public class FormularAnswers
        {
            public int UserId { get; set; }
            public int FormularId { get; set; }
            public int QuestionId { get; set; }
            public string QuestionString { get; set; }
            public string QuestionAnswer { get; set; }
            public DateTime DateSubmitted { get; set; }
        }
    }
}
