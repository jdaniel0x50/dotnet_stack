using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using form_submission.Models;
using Newtonsoft.Json;

namespace form_submission.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("errors") != null)
            {
                var deserialized = JsonConvert.DeserializeObject<Dictionary<string, Object>>(HttpContext.Session.GetString("errors"));
                ViewBag.errors = deserialized;                
            }
            return View();
        }

        [HttpPost]
        [Route("form_process")]
        public IActionResult form_process(string firstName, string lastName, int age, string email, string password, string password_conf)
        {
            bool errorsPresent = false;
            if (password != password_conf)
            {
                ModelState.AddModelError("password", "The password and confirm password do not match.");
                errorsPresent = true;
                // Console.WriteLine("KEYS_______");
                // Dictionary<string, string> errorDict = new Dictionary<string, string>();
                // foreach (string key in ModelState.Keys)
                // {
                //     Console.WriteLine(key);
                //     Console.WriteLine(ModelState[key].Errors.Count);
                //     if (ModelState[key].Errors.Count > 0)
                //     {
                //         foreach (var error in ModelState[key].Errors)
                //         {
                //             Console.WriteLine(error.ErrorMessage);
                //             errorDict[key] = error.ErrorMessage;
                //         }
                //     }
                // }
                // string errorsDeserialized = JsonConvert.SerializeObject<Dictionary<string, string>>(errorDict);
                // Console.WriteLine("VALUES_______");
                // Console.WriteLine(ModelState["password"].Errors[0].ErrorMessage);
            }
            User NewUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                Password = password
            };
            bool resultValid = TryValidateModel(NewUser);
            if (!resultValid || errorsPresent)
            {
                string errorsSerialized = JsonConvert.SerializeObject(ModelState);
                HttpContext.Session.SetString("errors", errorsSerialized);
                return RedirectToAction("Index");
            }
            string userSerialized = JsonConvert.SerializeObject(NewUser);
            HttpContext.Session.SetString("user", userSerialized);
            return RedirectToAction("Success");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            User registeredUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            ViewBag.firstName = registeredUser.FirstName;
            ViewBag.lastName = registeredUser.LastName;
            ViewBag.age = registeredUser.Age;
            ViewBag.email = registeredUser.Email;
            return View();
        }
    }
}
