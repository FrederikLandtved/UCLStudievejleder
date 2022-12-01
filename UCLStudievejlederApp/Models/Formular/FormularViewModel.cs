using UCLStudievejlederApp.Models.Generic;

namespace UCLStudievejlederApp.Models.Formular
{
    public class FormularViewModel
    {
        public List<FormularAnswerModel> Questions { get; set; } = new List<FormularAnswerModel>();
    }

    public class FormularAnswerModel
    {
        public int QuestionId { get; set; }
        public List<UCLSelectModel> Options { get; set; } = new List<UCLSelectModel>();
    }
}
