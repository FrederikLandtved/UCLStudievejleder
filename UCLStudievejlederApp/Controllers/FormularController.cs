﻿using Microsoft.AspNetCore.Mvc;

namespace UCLStudievejlederApp.Controllers
{
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
    }
}