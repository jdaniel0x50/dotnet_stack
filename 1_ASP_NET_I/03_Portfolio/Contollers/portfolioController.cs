using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_Portfolio.Controllers
{
    public class portfolioController : Controller
    {
        [HttpGet]
        [Route("home")]
        public IActionResult home()
        {
            return View();
        }

        [HttpGet]
        [Route("projects")]
        public IActionResult projects()
        {
            return View();
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult contact()
        {
            return View();
        }

        [HttpGet]
        [Route("survey")]
        public IActionResult survey()
        {
            return View();
        }

        [HttpPost]
        [Route("form_process")]
        public IActionResult success(string name, string location, string language, string comment)
        {
            ViewBag.name = name;
            ViewBag.location = location;
            ViewBag.language = language;
            ViewBag.comment = comment;
            return View();
        }

    }
}
