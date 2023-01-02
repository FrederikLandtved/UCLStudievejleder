using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Institution;
using DatabaseAccess.Institution.Models;
using DatabaseAccess.User;
using DatabaseAccess.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Data;
using UCLStudievejlederApp.Models.Generic;
using UCLStudievejlederApp.Models.User;
using static DatabaseAccess.FieldOfStudy.FieldOfStudyDb;
using static DatabaseAccess.User.UserDb;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UCLStudievejlederApp.Controllers
{
    public class UserController : Controller
    {
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly UserDb _userDb;
        private readonly InstitutionDb _institutionDb;
        private readonly FieldOfStudyDb _fieldOfStudyDb;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, FieldOfStudyDb fieldOfStudyDb, InstitutionDb institutionDb, UserDb userDb)
        {
            _userManager = userManager;
            _signManager = signManager;
            _userDb = userDb;
            _institutionDb = institutionDb;
            _fieldOfStudyDb = fieldOfStudyDb;
    }

        [HttpGet]
        public IActionResult CreateUser()
        {
            RegisterUserModel registerUserModel = new RegisterUserModel();

            foreach(InstitutionModel institution in _institutionDb.GetAllInstitutions())
                registerUserModel.AllInstitutions.Add(new UCLSelectModel { Id = institution.InstitutionId, Name = institution.Name });
            
            foreach (FieldOfStudyModel fieldOfStudy in _fieldOfStudyDb.GetAllFieldsOfStudy())
                registerUserModel.AllFieldsOfStudy.Add(new UCLSelectModel { Id = fieldOfStudy.FieldOfStudyId, Name = fieldOfStudy.Name });
           

            return View(registerUserModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    foreach (UCLSelectModel userInstitution in model.AllInstitutions)
                    {
                        if (userInstitution.IsSelected == true)
                            _institutionDb.LinkUserToInstitution(user.UserId, userInstitution.Id);
                    }

                    foreach (UCLSelectModel userFieldOfStudy in model.AllFieldsOfStudy)
                    {
                        if (userFieldOfStudy.IsSelected == true)
                            _fieldOfStudyDb.LinkUserToFieldOfStudy(user.UserId, userFieldOfStudy.Id);
                    }

                    model.SuccessMessage = $"Brugeren {model.FirstName} {model.LastName} blev oprettet.";

                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult EditUser()
        {
            List<UserMinimalModel> model = _userDb.GetMinimalUserList();

            return View(model);
        }

        [HttpGet]
        public IActionResult EditOneUser(int userId)
        {

            UserModel user = _userDb.GetUser(userId);
            user.FieldOfStudies = _fieldOfStudyDb.GetFieldsOfStudyByUserId(userId);
            user.Institutions = _institutionDb.GetInstitutionsByUserId(userId);

            RegisterUserModel model = new RegisterUserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.UserId
            };

            foreach (InstitutionModel institution in _institutionDb.GetAllInstitutions())
                model.AllInstitutions.Add(new UCLSelectModel { Id = institution.InstitutionId, Name = institution.Name, IsSelected = user.Institutions.Any(x => x.InstitutionId == institution.InstitutionId) });

            foreach (FieldOfStudyModel fieldOfStudy in _fieldOfStudyDb.GetAllFieldsOfStudy())
                model.AllFieldsOfStudy.Add(new UCLSelectModel { Id = fieldOfStudy.FieldOfStudyId, Name = fieldOfStudy.Name, IsSelected = user.FieldOfStudies.Any(x => x.FieldOfStudyId == fieldOfStudy.FieldOfStudyId) });

            return View(model);
        }
        
        [HttpPost]
        public IActionResult EditOneUser(RegisterUserModel model)
        {
            UserModel user = _userDb.GetUser(model.UserId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            _userDb.UpdateUser(user);

            return RedirectToAction("EditOneUser", new { userId = model.UserId });
        }

    }
}

