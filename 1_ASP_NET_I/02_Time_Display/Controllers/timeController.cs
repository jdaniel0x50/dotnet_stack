using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _02_Time_Display.Controllers
{
    public class timeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult time()
        {
            DateTime CurrentTime = DateTime.Now.ToString("D");
            var AnonObject = new
            {
                date = "DATE",
                time = "TIME"
            };
            return View();
        }
    }
}
