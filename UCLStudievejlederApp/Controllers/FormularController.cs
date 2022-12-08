using DatabaseAccess.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Models.Formular;
using UCLStudievejlederApp.Models.Generic;
using static DatabaseAccess.Question.AnswerOptionDb;
using static DatabaseAccess.Question.QuestionDb;

namespace UCLStudievejlederApp.Controllers
{
    [Authorize]
    public class FormularController : Controller
    {
        private readonly QuestionDb _questionDb;
        public FormularController(QuestionDb questionDb)
        {
            _questionDb = questionDb;
        }

        public IActionResult Index()
        {
            FormularViewModel model = new FormularViewModel();
            model.Questions = _questionDb.GetAllQuestionsWithAnswers();

            for (int i = -12; i <= 0; i++)
            {
                model.Questions[0].AnswerOptions.Add(new AnswerOption { QuestionId = 1, AnswerOptionString = DateTime.Now.AddMonths(i).ToString("MMMM yyyy") });
            }

            return View(model);
        }

        public IActionResult FormularList()
        {
            return View();
        }

        public IActionResult EditFormular()
        {
            return View();
        }
    }
}
