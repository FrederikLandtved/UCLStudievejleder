using DatabaseAccess.Generic;
using DatabaseAccess.Question;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using static DatabaseAccess.Generic.GenericSql;
using static DatabaseAccess.Question.AnswerOptionDb;

//undsky
namespace DatabaseAccess.Formulars
{
    public class FormularDb
    {
        private readonly GenericSql _genericSql;

        public FormularDb() { 
        
            _genericSql = new GenericSql();
        }

        public List<Formulars> GetAllFormulars()
        {
            List<Formulars> formulars = new();
            

            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[Formular]");

            while (reader.Read())
            {
                formulars.Add(new Formulars
                {
                    FormularId = reader.GetInt32(0)

                });
            }
            reader.Close();
            return formulars;
        }


        public class Formulars
        {
            public int FormularId { get; set; }
            
        }

  

        public int GetAmountOfAnswers(int optionId, int questionId)
        {
            int amountOfRows = 0;

            SqlDataReader reader = _genericSql.ExecuteSproc("SelectAllAnswers", new List<ParameterModel>
            {
                new ParameterModel{ Parameter = "@OptionID", Value = optionId.ToString()},
                new ParameterModel{ Parameter = "@QuestionID", Value = questionId.ToString()},
            });

            while (reader.Read())
                amountOfRows++;
            reader.Close();
            return amountOfRows;
        }


    }


}
