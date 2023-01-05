using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.FormularDbAccess;
using DatabaseAccess.Institution;
using DatabaseAccess.Institution.Models;
using DatabaseAccess.Question;
using DatabaseAccess.QuestionAnswer;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using UCLStudievejlederApp.Models.Formular;
using static DatabaseAccess.Question.AnswerOptionDb;
using static DatabaseAccess.Question.QuestionDb;

namespace UCLStudievejlederApp.Controllers
{
    [Authorize]
    public class FormularController : Controller
    {
        private readonly AnswerOptionDb _answerOptionDb;
        private readonly QuestionDb _questionDb;
        private readonly FormularDbAccess _formularDbAccess;
        private readonly UserDb _userDb;
        private readonly FieldOfStudyDb _fieldOfStudyDb;
        private readonly InstitutionDb _institutionDb;
        private readonly IMemoryCache _memoryCache;

        public FormularController(QuestionDb questionDb, FormularDbAccess formularDbAccess, UserDb userDb, AnswerOptionDb answerOptionDb, FieldOfStudyDb fieldOfStudyDb, InstitutionDb institutionDb, IMemoryCache memoryCache)
        {
            _questionDb = questionDb;
            _formularDbAccess = formularDbAccess;
            _userDb = userDb;
            _answerOptionDb = answerOptionDb;
            _fieldOfStudyDb = fieldOfStudyDb;
            _institutionDb = institutionDb;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _userDb.GetUserId(userToken);

            FormularViewModel model = new FormularViewModel();
            //var cachedModel = _memoryCache.Get<FormularViewModel>("QuestionModel");

            //if (cachedModel != null)
            //    return View(cachedModel);

            model.Questions = _questionDb.GetAllQuestionsWithAnswers();
            string currentDate = DateTime.Now.ToString("MMMM yyyy");

            for (int i = -1; i <= 0; i++)
            {
                string dateString = DateTime.Now.AddMonths(i).ToString("MMMM yyyy");
                dateString = char.ToUpper(dateString[0]) + dateString.Substring(1).ToLower();
                model.Questions[0].AnswerOptions.Add(new AnswerOption { QuestionId = 1, AnswerOptionString = dateString, AnswerOptionId = i + 12, IsSelected = (currentDate == dateString) });
            }

            //_memoryCache.Set("QuestionModel", model, TimeSpan.FromMinutes(5));

            SetInstitutionsAndFieldsOfStudy(userId, model);
            return View(model);
        }

        private void SetInstitutionsAndFieldsOfStudy(int userId, FormularViewModel model)
        {
            List<FieldOfStudyModel> usersFieldsOfStudy = _fieldOfStudyDb.GetFieldsOfStudyByUserId(userId);
            foreach (AnswerOption option in model.Questions[6].AnswerOptions)
                if (usersFieldsOfStudy.Any(x => x.Name == option.AnswerOptionString))
                    option.IsFavorite = true;

            List<InstitutionModel> usersInstitutions = _institutionDb.GetInstitutionsByUserId(userId);
            if (usersInstitutions.Count == 1)
            {
                foreach (AnswerOption option in model.Questions[5].AnswerOptions)
                    if (usersInstitutions.Any(x => x.Name == option.AnswerOptionString))
                        model.Questions[5].SingleChosenOption = option.AnswerOptionId;
            }
        }

        [HttpPost] 
        public IActionResult PostFormular(FormularViewModel model)
        {
            // Get userId so we know who the formular was created by
            var userToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _userDb.GetUserId(userToken);
            int formularId = 0;

            foreach (var item in model.Questions)
            {
                if (item.SingleChosenOption > 0)
                {
                    AnswerOption option = item.AnswerOptions.Where(x => x.AnswerOptionId == item.SingleChosenOption).First();
                    option.IsSelected = true;
                }

                DateTime dateSubmitted;

                if (DateTime.TryParse(item.DropdownChosenOption, out dateSubmitted))
                {
                    dateSubmitted = DateTime.Parse(item.DropdownChosenOption);
                    formularId = _formularDbAccess.CreateNewFormular(userId, dateSubmitted);
                }
            }

            // Create answer for each answer from model where IsSelected = true
            foreach (Question questionAndOptions in model.Questions)
            {
                foreach (AnswerOption option in questionAndOptions.AnswerOptions)
                {
                    if (option.IsSelected)
                        _questionDb.CreateQuestionAnswer(option.QuestionId, option.AnswerOptionId, formularId);
                }
            }

            return RedirectToAction("FormularList");
        }

        public IActionResult FormularList()
        {
            var userToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _userDb.GetUserId(userToken);

            MyFormularsModel model = new MyFormularsModel();
            List<FormularAnswers> answers = _answerOptionDb.GetFormularAnswers(userId);

            int currentFormularId = 0;
            for (int i = 0; i < answers.Count(); i++)
            {
                if (currentFormularId != answers[i].FormularId)
                {
                    currentFormularId = answers[i].FormularId;
                    model.Formulars.Add(new FormularListItemModel
                    {
                        Month = answers[i].DateSubmitted.ToString("MMMM yyyy"),
                        TypeOfEnquiry = answers[i].QuestionAnswer,
                        ConversationTime = answers[i + 1].QuestionAnswer,
                        Level = answers[i + 2].QuestionAnswer,
                        WhoWereGuided = answers[i + 3].QuestionAnswer,
                        ChoosePrimaryInstitution = answers[i + 4].QuestionAnswer,
                        ChooseAllFieldsOfStudies = answers[i + 5].QuestionAnswer,
                        SubjectCategory = answers[i + 6].QuestionAnswer,
                    });
                }
            }


            return View(model);
        }

        public IActionResult EditFormular()
        {
            return View();
        }
    }
}
