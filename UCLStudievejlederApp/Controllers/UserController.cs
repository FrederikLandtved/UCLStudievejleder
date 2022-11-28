using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Institution;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Data;
using UCLStudievejlederApp.Models.User;

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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            ModelState.Remove("AllInstitutions");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    InstitutionDb db = new InstitutionDb();
                    foreach(Institution userInstitution in model.AllInstitutions)
                    {
                        if (userInstitution.IsSelected == true)
                            db.LinkUserToInstitution(user.UserId, userInstitution.InstitutionId);
                    }

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return RedirectToAction("CreateUser");
        }


        public IActionResult CreateUser()
        {
            RegisterUserModel registerUserModel = new RegisterUserModel();

            InstitutionDb db = new InstitutionDb();
            registerUserModel.AllInstitutions = db.GetAllInstitutions();
            
            return View(registerUserModel);
        }

        public IActionResult EditUser()
        {
            return View();
        }
    }
}

