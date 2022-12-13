using DatabaseAccess.Formulars;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Models;
using static DatabaseAccess.Formulars.FormularDb;

namespace UCLStudievejlederApp.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            StatisticViewModel statisticViewModel = new StatisticViewModel();
            FormularDb formularDb = new FormularDb();
            statisticViewModel.AllFormulars = formularDb.GetAllFormulars().Count();

            double amountOfPersonal = formularDb.GetAmountOfAnswers(1,2);
            double personalPercent = Math.Round((amountOfPersonal / statisticViewModel.AllFormulars) * 100, 1); 

            statisticViewModel.PersonalFormulars = personalPercent;

            

            double amountOfPhone = formularDb.GetAmountOfAnswers(2, 2);
            double phonePercent = Math.Round((amountOfPhone / statisticViewModel.AllFormulars) * 100, 1);


            statisticViewModel.PhoneFormulars = phonePercent;

            statisticViewModel.VirtuelleFormulars = formularDb.GetAmountOfAnswers(3, 2);
            return View(statisticViewModel);
        }

    }
}
