using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UCLStudievejlederApp.Models.User;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UCLStudievejlederApp.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult CreateUser()
        {
            RegisterUserModel model = new RegisterUserModel();

            return View(model);
        }

        public IActionResult EditUser()
        {
            return View();
        }
    }
}

