using System;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        [Route("passcode")]
        public IActionResult passcode()
        {
            int length = 14;
            string RandomString = GetRandomString(length);
            ViewBag.RandomString = RandomString;
            
            if (HttpContext.Session.GetInt32("passcode_num") == null) {
                HttpContext.Session.SetInt32("passcode_num", 1);
            }
            else {
                int? num = HttpContext.Session.GetInt32("passcode_num");
                num += 1;
                HttpContext.Session.SetInt32("passcode_num", Convert.ToInt32(num));
            }
            ViewBag.iterator = HttpContext.Session.GetInt32("passcode_num");
            return View();
        }

        [HttpGet]
        [Route("passcode_clear")]
        public IActionResult passcode_clear()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("passcode");
        }

    }
}
