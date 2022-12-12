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

        [HttpGet]
        public IActionResult Index()
        {
            FormularViewModel model = new FormularViewModel();
            model.Questions = _questionDb.GetAllQuestionsWithAnswers();
            string currentDate = DateTime.Now.ToString("MMMM yyyy");

            for (int i = -12; i <= 0; i++)
            {
                string dateString = DateTime.Now.AddMonths(i).ToString("MMMM yyyy");
                model.Questions[0].AnswerOptions.Add(new AnswerOption { QuestionId = 1, AnswerOptionString = dateString, AnswerOptionId = i + 12, IsSelected = (currentDate == dateString) });
            }

            return View(model);
        }

        [HttpPost] 
        public IActionResult PostFormular(FormularViewModel model)
        {
            return null;
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
