using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.Institution;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Data;
using UCLStudievejlederApp.Models.Generic;
using UCLStudievejlederApp.Models.User;
using static DatabaseAccess.FieldOfStudy.FieldOfStudyDb;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UCLStudievejlederApp.Controllers
{
    public class UserController : Controller
    {
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            RegisterUserModel registerUserModel = new RegisterUserModel();

            InstitutionDb institutionDb = new InstitutionDb();
            FieldOfStudyDb fieldOfStudyDb = new FieldOfStudyDb();

            foreach(Institution institution in institutionDb.GetAllInstitutions())
                registerUserModel.AllInstitutions.Add(new UCLSelectModel { Id = institution.InstitutionId, Name = institution.Name });
            
            foreach (FieldOfStudy fieldOfStudy in fieldOfStudyDb.GetAllFieldsOfStudy())
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
                    InstitutionDb institutionDb = new InstitutionDb();
                    FieldOfStudyDb fieldOfStudyDb = new FieldOfStudyDb();

                    foreach (UCLSelectModel userInstitution in model.AllInstitutions)
                    {
                        if (userInstitution.IsSelected == true)
                            institutionDb.LinkUserToInstitution(user.UserId, userInstitution.Id);
                    }

                    foreach (UCLSelectModel userFieldOfStudy in model.AllFieldsOfStudy)
                    {
                        if (userFieldOfStudy.IsSelected == true)
                            fieldOfStudyDb.LinkUserToFieldOfStudy(user.UserId, userFieldOfStudy.Id);
                    }

                    model.SuccessMessage = $"Brugeren {model.FirstName} {model.LastName} blev oprettet.";

                    return View(model);
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        public IActionResult EditUser()
        {
            return View();
        }
    }
}

