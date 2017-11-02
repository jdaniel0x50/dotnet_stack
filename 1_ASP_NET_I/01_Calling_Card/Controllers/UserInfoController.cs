using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01_Calling_Card.Controllers
{
    public class UserInfoController : Controller
    {
        [HttpGet]
        [Route("{firstName}/{lastName}/{age:int}/{favColor}")]
        public JsonResult Method(string firstName, string lastName, int age, string favColor)
        {
            var AnonObject = new {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                FavColor = favColor
            };
            return Json(AnonObject);
        }
    }
}
