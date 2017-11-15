using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _0_setup.Controllers
{
    public class HelloController : Controller
    {
        [HttpGetAttribute]
        // public string Index()
        // {
        //     return "Hello World! I'm in the Dojo";
        // }

        // A GET method
        [HttpGet]
        [Route("index")]
        public string Index()
        {
            return "Hello World!";
        }

        // [HttpGet]
        // [Route("template/{Name}")]
        // public IActionResult Method(string Name)
        // {
        //     // Method body
        // }

        // A POST method
        // [HttpPost]
        // [Route("")]
        // public IActionResult Other()
        // {
        //     // Return a view (We'll learn how soon!)
        // }
    }
}
