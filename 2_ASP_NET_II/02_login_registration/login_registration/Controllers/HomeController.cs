using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using login_registration.Models;
using login_registration.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace login_registration.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        [ImportModelState]
        public IActionResult Index()
        {
            // if (HttpContext.Session.GetString("errors") != null)
            if (TempData["errors"] != null)
            {
                // string stringSerialized = TempData["errors"];
                // var deserialized = JsonConvert.DeserializeObject<Dictionary<string, Object>>(TempData["errors"]);
                
                // modelState = TempData["ModelState"];
                // ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);
                ViewBag.errors = ModelState;
                // System.Console.WriteLine(TempData["errors"]);
                // System.Console.WriteLine(storedModelState.ErrorCount);
                // ViewBag.errors = storedModelState;
                // foreach (string key in storedModelState.Keys)
                // {
                //     System.Console.WriteLine(key);
                //     if (key == "FirstName" && storedModelState[key].Errors.Count > 0)
                //     {
                //         foreach (var error in storedModelState[key].Errors)
                //         {
                //             System.Console.WriteLine(error.ErrorMessage);
                //         }
                //     }
                // }
            }
            if (TempData["login_errors"] != null)
            {
                ViewBag.login_errors = ModelState;
            }
            return View();
        }

        // POST: /login
        [HttpPost]
        [Route("login")]
        [ExportModelState]
        public IActionResult Login(string Username, string Password)
        {
            // query _dbConnector for the username and password submitted
            string query = "SELECT * FROM users ";
            query += $"WHERE username = '{Username}' AND password = '{Password}'";
            try
            {
                List<Dictionary<string, object>> allMatchUsers = _dbConnector.Query(query);
                string FirstName = "";
                if (allMatchUsers.Count == 1)
                {
                    // ensure there is only one matching result
                    // if there are more return to login with error
                    foreach (Dictionary<string, object> user in allMatchUsers)
                    {
                        foreach (string key in user.Keys)
                        {
                            if (key == "first_name")
                            {
                                FirstName = (string)user[key];
                            }
                        }
                    }
                    HttpContext.Session.SetString("LoggedIn_Username", Username);
                    HttpContext.Session.SetString("LoggedIn_FirstName", FirstName);
                    return RedirectToAction("LoginSuccess");
                }
                else
                {
                    string key = "login";
                    string errorMessage = "There was a problem with your account. Please contact the account administrator.";
                    ModelState.AddModelError(key, errorMessage);
                }
            }
            catch
            {
                // the username and password combination were not found
                string key = "login";
                string errorMessage = "The username and password combination you provided were not valid.";
                ModelState.AddModelError(key, errorMessage);
            }
            // if login was not successful, return to index with errors exported in modelstate
            TempData["login_errors"] = true;
            return RedirectToAction("Index");
        }

        // GET: /logout
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // POST: /register
        [HttpPost]
        [Route("register")]
        [ExportModelState]
        public IActionResult Register(User NewUser)
        {
            if (ModelState.IsValid)
            {
                // model validated correctly --> success
                string userSerialized = JsonConvert.SerializeObject(NewUser);
                TempData["user"] = (string)userSerialized;

                // add user to the _dbConnector
                string query = "INSERT INTO users ";
                query += "(username, first_name, last_name, email, birthdate, password) ";
                query += $"VALUES('{NewUser.Username}', '{NewUser.FirstName}', '{NewUser.LastName}', '{NewUser.Email}', STR_TO_DATE('{NewUser.Birthdate}', '%m/%d/%Y %h:%i:%s %p'), '{NewUser.Password}')";
                _dbConnector.Execute(query);

                // store user first name, and username in session
                // login to the application
                HttpContext.Session.SetString("LoggedIn_Username", NewUser.Username);
                HttpContext.Session.SetString("LoggedIn_FirstName", NewUser.FirstName);
                return RedirectToAction("Success");
            }
            // model did not validate correctly --> show errors to user

            // string errorsSerialized = JsonConvert.SerializeObject(ModelState);
            // TempData["errors"] = errorsSerialized;
            // TempData["ModelState"] = ModelState;

            // ModelStateDictionary storedModelState = new ModelStateDictionary();
            // storedModelState = ModelState;
            TempData["errors"] = true;
            return RedirectToAction("Index");
        }

        // GET: successful registration
        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            User NewUser = JsonConvert.DeserializeObject<User>((string)TempData["user"]);
            ViewBag.User = NewUser;
            return View();
        }

        // GET: successful login
        [HttpGet]
        [Route("loginsuccess")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

    }
}
