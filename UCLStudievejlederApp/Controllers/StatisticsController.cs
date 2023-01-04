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


            double amountOfVirtuelle = formularDb.GetAmountOfAnswers(3, 2);
            double virtuellePercent = Math.Round((amountOfVirtuelle / statisticViewModel.AllFormulars) * 100, 1);


            statisticViewModel.VirtuelleFormulars = virtuellePercent;

            //Hvor mange procent af alle besvarelser har haft en samtaletid på 1-20 min.

            double amountOfTimeUseOne = formularDb.GetAmountOfAnswers(4, 3);
            double timeUseOne = Math.Round((amountOfTimeUseOne / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TimeUseOneFormulars = timeUseOne;

            double amountOfTimeUseTwo = formularDb.GetAmountOfAnswers(5, 3);
            double timeUseTwo = Math.Round((amountOfTimeUseTwo / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TimeUseTwoFormulars = timeUseTwo;

            double amountOfTimeUseThree = formularDb.GetAmountOfAnswers(6, 3);
            double timeUseThree = Math.Round((amountOfTimeUseThree / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TimeUseThreeFormulars = timeUseThree;

            return View(statisticViewModel);
        }

    }
}
