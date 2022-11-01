using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UCLStudievejlederApp.Models;

namespace UCLStudievejlederApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}