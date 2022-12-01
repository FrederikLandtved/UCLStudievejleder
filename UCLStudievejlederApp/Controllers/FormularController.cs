using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Models.Formular;
using UCLStudievejlederApp.Models.Generic;

namespace UCLStudievejlederApp.Controllers
{
    [Authorize]
    public class FormularController : Controller
    {
        public IActionResult Index()
        {
            FormularViewModel model = new FormularViewModel();

            model.Questions.Add(new FormularAnswerModel { QuestionId = 1 });

            for (int i = -12; i <= 0; i++)
            {
                model.Questions[0].Options.Add(new UCLSelectModel { Id = i + 13, Name = DateTime.Now.AddMonths(i).ToString("MMMM yyyy") });
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
