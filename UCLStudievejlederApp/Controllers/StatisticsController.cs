using DatabaseAccess.Formulars;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Models;
using static DatabaseAccess.Formulars.FormularDb;

namespace UCLStudievejlederApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly FormularDb _formularDb;
        public StatisticsController(FormularDb formularDb)
        {
            _formularDb = formularDb;
        }
        public IActionResult Index()
        {
            StatisticViewModel statisticViewModel = new StatisticViewModel();
            statisticViewModel.AllFormulars = _formularDb.GetAllFormulars().Count();

            double amountOfPersonal = _formularDb.GetAmountOfAnswers(1,2);
            double personalPercent = Math.Round((amountOfPersonal / statisticViewModel.AllFormulars) * 100, 1); 

            statisticViewModel.PersonalFormulars = personalPercent;

            

            double amountOfPhone = _formularDb.GetAmountOfAnswers(2, 2);
            double phonePercent = Math.Round((amountOfPhone / statisticViewModel.AllFormulars) * 100, 1);


            statisticViewModel.PhoneFormulars = phonePercent;

            statisticViewModel.VirtuelleFormulars = _formularDb.GetAmountOfAnswers(3, 2);
            return View(statisticViewModel);
        }

    }
}
