using DatabaseAccess.FormularDbAccess;
using DatabaseAccess.Question;
using DatabaseAccess.QuestionAnswer;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using UCLStudievejlederApp.Models.Formular;
using UCLStudievejlederApp.Models.Generic;
using static DatabaseAccess.FormularDbAccess.FormularDbAccess;
using static DatabaseAccess.Question.AnswerOptionDb;
using static DatabaseAccess.Question.QuestionDb;
using static DatabaseAccess.QuestionAnswer.QuestionAnswerDb;

namespace UCLStudievejlederApp.Controllers
{
    [Authorize]
    public class FormularController : Controller
    {
        private readonly AnswerOptionDb _answerOptionDb;
        private readonly QuestionDb _questionDb;
        private readonly QuestionAnswerDb _questionAnswerDb;
        private readonly FormularDbAccess _formularDbAccess;
        private readonly UserDb _userDb;

        public FormularController(QuestionDb questionDb, FormularDbAccess formularDbAccess, UserDb userDb, QuestionAnswerDb questionAnswerDb, AnswerOptionDb answerOptionDb)
        {
            _questionDb = questionDb;
            _formularDbAccess = formularDbAccess;
            _userDb = userDb;
            _questionAnswerDb = questionAnswerDb;
            _answerOptionDb = answerOptionDb;
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

            // Create answer for each answer from model
            foreach (Question questionAndOptions in model.Questions)
            {
                foreach (AnswerOption option in questionAndOptions.AnswerOptions)
                {
                    if (option.IsSelected)
                        _questionDb.CreateQuestionAnswer(option.QuestionId, option.AnswerOptionId, formularId);
                }
            }

            return RedirectToAction("Index");
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
                        Month = answers[i].DateSubmitted.ToString(),
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

        private string GetAnswerFromQuestionAnswerId(List<QuestionAnswerModel> answers)
        {
            string fullString = "";

            foreach (QuestionAnswerModel answer in answers)
            {
                fullString += _answerOptionDb.GetAnswerOptionString(answer.AnswerOptionId);
                if (answer != answers.Last())
                {
                    fullString += ", ";
                }
            }

            return fullString;
        }

        public IActionResult EditFormular()
        {
            return View();
        }
    }
}
