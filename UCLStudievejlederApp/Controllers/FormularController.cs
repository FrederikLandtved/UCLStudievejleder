using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UCLStudievejlederApp.Controllers
{
    [Authorize]
    public class FormularController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormularList()
        {
            return View();
        }

        public IActionResult EditFormular()
        {
            return View();
        }
    }
}
