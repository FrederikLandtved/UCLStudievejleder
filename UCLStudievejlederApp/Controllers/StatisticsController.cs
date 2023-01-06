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
            //hvor mange procent hevendelser er der totalt

            StatisticViewModel statisticViewModel = new StatisticViewModel();
            FormularDb formularDb = new FormularDb();

            //-----Hvor mangehenvendelser i procent per afdeling------
            statisticViewModel.AllFormulars = formularDb.GetAllFormulars().Count();

            


            //Fredericia
            double amountOfFredericia = formularDb.GetAmountOfAnswers(13, 6);
            double countFredericia = Math.Round((amountOfFredericia / statisticViewModel.AllFormulars) * 100, 1);
          
            statisticViewModel.amountOfFredericiaFormulars = countFredericia;

            //Jelling
            double amountOfJelling = formularDb.GetAmountOfAnswers(14, 6);
            double countJelling = Math.Round((amountOfJelling / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.amountOfJellingFormulars = countJelling;

            //Odense
            double amountOfOdense = formularDb.GetAmountOfAnswers(15, 6);
            double countOdense = Math.Round((amountOfOdense / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.amountOfOdenseFormulars = countOdense;

            //Svendborg
            double amountOfSvendborg = formularDb.GetAmountOfAnswers(16, 6);
            double countSvendborg = Math.Round((amountOfSvendborg / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.amountOfSvendborgFormulars = countSvendborg;

            //Vejle
            double amountOfVejle = formularDb.GetAmountOfAnswers(17, 6);
            double countVejle = Math.Round((amountOfVejle / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.amountOfVejleFormulars = countVejle;




            //------Hvor mange procent af henvendelserne er telefonisk/virtuelle/personlige------

            double amountOfPersonal = formularDb.GetAmountOfAnswers(1, 2);
            double personalPercent = Math.Round((amountOfPersonal / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.PersonalFormulars = personalPercent;

            double amountOfPhone = formularDb.GetAmountOfAnswers(2, 2);
            double phonePercent = Math.Round((amountOfPhone / statisticViewModel.AllFormulars) * 100, 1);


            statisticViewModel.PhoneFormulars = phonePercent;


            double amountOfVirtuelle = formularDb.GetAmountOfAnswers(3, 2);
            double virtuellePercent = Math.Round((amountOfVirtuelle / statisticViewModel.AllFormulars) * 100, 1);


            statisticViewModel.VirtuelleFormulars = virtuellePercent;

            //-------Hvor mange procent af alle besvarelser har haft en samtaletid----


            //1-20 min. i procent
            double amountOfTimeUseOne = formularDb.GetAmountOfAnswers(4, 3);
            double timeUseOne = Math.Round((amountOfTimeUseOne / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TimeUseOneFormulars = timeUseOne;

            //1-20 min. i antal
            double amountOfTimeUseOneCount = formularDb.GetAmountOfAnswers(4, 3);
            double timeUseOneCount = Math.Round((amountOfTimeUseOneCount *20)/60, 1);

            statisticViewModel.TimeUseOneCountFormulars = timeUseOneCount;


            //21-40 min. i procent
            double amountOfTimeUseTwo = formularDb.GetAmountOfAnswers(5, 3);
            double timeUseTwo = Math.Round((amountOfTimeUseTwo / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TimeUseTwoFormulars = timeUseTwo;

            //21-40 min. i antal
            double amountOfTimeUseTwoCount = formularDb.GetAmountOfAnswers(5, 3);
            double timeUseTwoCount = Math.Round((amountOfTimeUseTwoCount * 40) / 60, 1);

            statisticViewModel.TimeUseTwoCountFormulars = timeUseTwoCount;


            //41-60min. i procent
            double amountOfTimeUseThree = formularDb.GetAmountOfAnswers(6, 3);
            double timeUseThree = Math.Round((amountOfTimeUseThree / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TimeUseThreeFormulars = timeUseThree;

            //41-60min. i antal

            double amountOfTimeUseThreeCount = formularDb.GetAmountOfAnswers(6, 3);
            double timeUseThreeCount = Math.Round((amountOfTimeUseThreeCount * 60) / 60, 1);

            statisticViewModel.TimeUseThreeCountFormulars = timeUseThreeCount;



            //Procentvis, hvor mange har har valgt emnet 


            //Administrative forhold i procent
            double amountOfSubAdmin = formularDb.GetAmountOfAnswers(69, 8);
            double adminSubPercent = Math.Round((amountOfSubAdmin / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.AdminSubFormulars = adminSubPercent;

            //Administrative forhold antal

            int amountOfSubAdminNr = formularDb.GetAmountOfAnswers(69, 8);

            statisticViewModel.AdminSubFormularsNr = amountOfSubAdminNr;
        

            //Barsel i procent
            double amountOfSubBarsel = formularDb.GetAmountOfAnswers(70, 8);
            double barselSubPercent = Math.Round((amountOfSubBarsel / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.BarselSubFormulars = barselSubPercent;

            //Barsel antal
            int amountOfSubBarselNr = formularDb.GetAmountOfAnswers(70, 8);

            statisticViewModel.BarselSubFormularsNr = amountOfSubBarselNr;

            //Eksamen i procent
            double amountOfSubEksamen = formularDb.GetAmountOfAnswers(71, 8);
            double eksamenSubPercent = Math.Round((amountOfSubEksamen / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.EksamenSubFormulars = eksamenSubPercent;

            //Eksamen antal
            int amountOfSubEksamenNr = formularDb.GetAmountOfAnswers(71, 8);

            statisticViewModel.EksamenSubFormularsNr = amountOfSubEksamenNr;


            //Fastholde trivsel i procent
            double amountOfSubTrivsel = formularDb.GetAmountOfAnswers(72, 8);
            double trivselSubPercent = Math.Round((amountOfSubTrivsel / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.TrivselSubFormulars = trivselSubPercent;

            //Fastholde trivsel antal
            int amountOfSubTrivselNr = formularDb.GetAmountOfAnswers(72, 8);

            statisticViewModel.TrivselSubFormularsNr = amountOfSubTrivselNr;

            //Internationale muligheder i procent
            double amountOfSubInternational = formularDb.GetAmountOfAnswers(73, 8);
            double internationalSubPercent = Math.Round((amountOfSubInternational / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.InternationalSubFormulars = internationalSubPercent;

            //Internationale muligheder antal
            int amountOfSubInternationalNr = formularDb.GetAmountOfAnswers(73, 8);

            statisticViewModel.InternationalSubFormularsNr = amountOfSubInternationalNr;

            //Mistrivsel i procent
            double amountOfSubMistrivsel = formularDb.GetAmountOfAnswers(74, 8);
            double mistrivselSubPercent = Math.Round((amountOfSubMistrivsel / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.MistrivselSubFormulars = mistrivselSubPercent;

            //Mistrivsel antal
            int amountOfSubMistrivselNr = formularDb.GetAmountOfAnswers(74, 8);

            statisticViewModel.MistrivselSubFormularsNr = amountOfSubMistrivselNr;

            //Optagelsesvejledning i procent
            double amountOfSubOptVej = formularDb.GetAmountOfAnswers(75, 8);
            double optVejSubPercent = Math.Round((amountOfSubOptVej/ statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.OptVejSubFormulars = optVejSubPercent;

            //Optagelsesvejledning antal
            int amountOfSubOptVejNr = formularDb.GetAmountOfAnswers(75, 8);

            statisticViewModel.OptVejSubFormularsNr = amountOfSubOptVejNr;

            //Ordensrelement i procent
            double amountOfSubOrdensrel = formularDb.GetAmountOfAnswers(76, 8);
            double ordensrelSubPercent = Math.Round((amountOfSubOrdensrel / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.OrdensrelSubFormulars = ordensrelSubPercent;

            //Ordensrelement antal
            int amountOfSubOrdensrelNr = formularDb.GetAmountOfAnswers(76, 8);

            statisticViewModel.OrdensrelSubFormularsNr = amountOfSubOrdensrelNr;

            //Orlov i procent
            double amountOfSubOrlov = formularDb.GetAmountOfAnswers(77, 8);
            double orlovSubPercent = Math.Round((amountOfSubOrlov / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.OrlovSubFormulars = orlovSubPercent;

            //Orlov antal
            int amountOfSubOrlovNr = formularDb.GetAmountOfAnswers(77, 8);

            statisticViewModel.OrlovSubFormularsNr = amountOfSubOrlovNr;

            //Overflytning/genindskrivning i procent
            double amountOfSubOverflytGenind = formularDb.GetAmountOfAnswers(78, 8);
            double overflytGenindSubPercent = Math.Round((amountOfSubOverflytGenind / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.OverflytGenindSubFormulars = overflytGenindSubPercent;

            //Overflytning/genindskrivning antal
            int amountOfSubOverflytGenindNr = formularDb.GetAmountOfAnswers(78, 8);

            statisticViewModel.OverflytGenindSubFormularsNr = amountOfSubOverflytGenindNr;

            //Personlige forhold i procent
            double amountOfSubPersonForh = formularDb.GetAmountOfAnswers(79, 8);
            double personForhSubPercent = Math.Round((amountOfSubPersonForh / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.PersonForhSubFormulars = personForhSubPercent;

            //Personlige forhold antal

            //Overflytning/genindskrivning antal
            int amountOfSubPersonForhNr = formularDb.GetAmountOfAnswers(79, 8);

            statisticViewModel.PersonForhSubFormularsNr = amountOfSubPersonForhNr;

            //Praktik i DK i procent
            double amountOfSubPraktik = formularDb.GetAmountOfAnswers(80, 8);
            double praktikSubPercent = Math.Round((amountOfSubPraktik / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.PraktikSubFormulars = praktikSubPercent;

            //Praktik i DK antal
            int amountOfSubPraktikNr = formularDb.GetAmountOfAnswers(80, 8);

            statisticViewModel.PraktikSubFormularsNr = amountOfSubPraktikNr;

            //Ikke relevent henvendelse i procent
            double amountOfSubIkRel = formularDb.GetAmountOfAnswers(81, 8);
            double ikRelSubPercent = Math.Round((amountOfSubIkRel / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.IkRelSubFormulars = ikRelSubPercent;

            //Ikke relevent henvendelse antal
            int amountOfSubIkRelNr = formularDb.GetAmountOfAnswers(81, 8);

            statisticViewModel.IkRelSubFormularsNr = amountOfSubIkRelNr;

            //SPS i procent
            double amountOfSubSPS = formularDb.GetAmountOfAnswers(82, 8);
            double sPSSubPercent = Math.Round((amountOfSubSPS / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.SPSSubFormulars = sPSSubPercent;

            //SPS antal
            int amountOfSubSPSNr = formularDb.GetAmountOfAnswers(82, 8);

            statisticViewModel.SPSSubFormularsNr = amountOfSubSPSNr;

            //Studieophør i procent
            double amountOfSubStudOph = formularDb.GetAmountOfAnswers(83, 8);
            double studOphSubPercent = Math.Round((amountOfSubStudOph / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.StudOphSubFormulars = studOphSubPercent;

            //Studieophør antal
            int amountOfSubStudOphNr = formularDb.GetAmountOfAnswers(83, 8);

            statisticViewModel.StudOphSubFormularsNr = amountOfSubStudOphNr;

            //Studieplanlægning i procent
            double amountOfSubStdPlan = formularDb.GetAmountOfAnswers(84, 8);
            double stdPlanSubPercent = Math.Round((amountOfSubStdPlan / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.StdPlanSubFormulars =stdPlanSubPercent;

            //Studieplanlægning antal
            int amountOfSubStdPlanNr = formularDb.GetAmountOfAnswers(84, 8);

            statisticViewModel.StdPlanSubFormularsNr = amountOfSubStdPlanNr;

            //Studietvivl/studievalg i procent
            double amountOfSubStdTvivl = formularDb.GetAmountOfAnswers(85, 8);
            double stdTvivlSubPercent = Math.Round((amountOfSubStdTvivl/ statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.StdTvivlSubFormulars = stdTvivlSubPercent;

            //Studietvivl/studievalg antal
            int amountOfSubStdTvivlNr = formularDb.GetAmountOfAnswers(85, 8);

            statisticViewModel.StdTvivlSubFormularsNr = amountOfSubStdTvivlNr;

            //Studieudfordringer i procent
            double amountOfSubStdUdf = formularDb.GetAmountOfAnswers(86, 8);
            double stdUdfSubPercent = Math.Round((amountOfSubStdUdf / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.StdUdfSubFormulars = stdUdfSubPercent;

            //Studieudfordringer antal
            int amountOfSubStdUdfNr = formularDb.GetAmountOfAnswers(86, 8);

            statisticViewModel.StdUdfSubFormularsNr = amountOfSubStdUdfNr;

            //Sygdom(egne) i procent
            double amountOfSubSygEgne = formularDb.GetAmountOfAnswers(87, 8);
            double sygEgneSubPercent = Math.Round((amountOfSubSygEgne / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.SygEgneSubFormulars = sygEgneSubPercent;

            //Sygdom(egne) antal
            int amountOfSubSygEgneNr = formularDb.GetAmountOfAnswers(87, 8);

            statisticViewModel.SygEgneSubFormularsNr = amountOfSubSygEgneNr;

            //Undervisningen (Samarbejdsvanskligheder) i procent
            double amountOfSubUnvSamaVan = formularDb.GetAmountOfAnswers(88, 8);
            double unvSamaVanSubPercent = Math.Round((amountOfSubUnvSamaVan / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.UnvSamaVanSubFormulars = unvSamaVanSubPercent;

            //Undervisningen (Samarbejdsvanskligheder) antal
            int amountOfSubUnvSamaVanNr = formularDb.GetAmountOfAnswers(88, 8);

            statisticViewModel.UnvSamaVanSubFormularsNr = amountOfSubUnvSamaVanNr;

            //Økonomi i procent
            double amountOfSubOkonomi = formularDb.GetAmountOfAnswers(89, 8);
            double okonomiSubPercent = Math.Round((amountOfSubOkonomi / statisticViewModel.AllFormulars) * 100, 1);

            statisticViewModel.OkonomiSubFormulars = okonomiSubPercent;

            //Økonomi antal
            int amountOfSubOkonomiNr = formularDb.GetAmountOfAnswers(89, 8);

            statisticViewModel.OkonomiSubFormularsNr = amountOfSubOkonomiNr;

            return View(statisticViewModel);
        }

    }
}
