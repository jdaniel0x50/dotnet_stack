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
            return View();
        }
    }
}
