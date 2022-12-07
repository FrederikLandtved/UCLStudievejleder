using UCLStudievejlederApp.Models.Generic;
using static DatabaseAccess.Question.QuestionDb;

namespace UCLStudievejlederApp.Models.Formular
{
    public class FormularViewModel
    {
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
