using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using dapper_trails.Models;
using dapper_trails.Factory;
using dapper_trails.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace dapper_trails.Controllers
{
    public class HomeController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /login
        //  /logout
        //  /register
        //  /success
        //  /loginsuccess
        // ########## ROUTES ##########

        private const string LOGGED_IN_ID = "LoggedIn_Id";
        private const string LOGGED_IN_USERNAME = "LoggedIn_Username";
        private const string LOGGED_IN_FIRSTNAME = "LoggedIn_FirstName";
        private readonly UserFactory userFactory;
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
            userFactory = new UserFactory();
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        [ImportModelState]
        public IActionResult Index()
        {
            if (TempData["errors"] != null)
            {
                ViewBag.errors = ModelState;
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
        public IActionResult Login(LoginRegFormModel userVM)
        {
            if (TryValidateModel(userVM.loginVM))
            {
                try
                {
                    User logging_user = userFactory.FindByLogin(userVM.loginVM.Username, userVM.loginVM.Password);
                    HttpContext.Session.SetInt32(LOGGED_IN_ID, logging_user.id);
                    HttpContext.Session.SetString(LOGGED_IN_USERNAME, userVM.loginVM.Username);
                    HttpContext.Session.SetString(LOGGED_IN_FIRSTNAME, logging_user.first_name);
                    return RedirectToAction("LoginSuccess");
                }
                catch
                {
                    // the username and password combination were not found
                    string key = "login";
                    string errorMessage = "The username and password combination you provided were not valid.";
                    ModelState.AddModelError(key, errorMessage);
                }
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
        public IActionResult Register(LoginRegFormModel userVM)
        {
            if (TryValidateModel(userVM.registerVM))
            {
                // model validated correctly --> success
                // confirm that a user does not exist with the selected username
                try
                {
                    User testUser = userFactory.FindByUsername(userVM.registerVM.Username);
                    if (testUser != null)
                    {
                        // the username currently exists in the database
                        string key = "Username";
                        string errorMessage = "This username already exists. Please select another or login.";
                        ModelState.AddModelError(key, errorMessage);
                        TempData["errors"] = true;
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    // if there was an error - the user was not found - do nothing and proceed
                }
                // confirm that a user does not exist with the selected email
                try
                {
                    User testUser = userFactory.FindByEmail(userVM.registerVM.Email);
                    if (testUser != null)
                    {
                        // the email currently exists in the database
                        string key = "Email";
                        string errorMessage = "This email address already exists. Please select another or login.";
                        ModelState.AddModelError(key, errorMessage);
                        TempData["errors"] = true;
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    // if there was an error - the user was not found - do nothing and proceed
                }
                userFactory.Add(userVM.registerVM);
                string userSerialized = JsonConvert.SerializeObject(userVM.registerVM);
                TempData["user"] = (string)userSerialized;

                // store user id, first name, and username in session
                // run query to gather id number generated by the database
                User NewUser = userFactory.FindByUsername(userVM.registerVM.Username);
                // login to the application
                HttpContext.Session.SetInt32(LOGGED_IN_ID, NewUser.id);
                HttpContext.Session.SetString(LOGGED_IN_USERNAME, userVM.registerVM.Username);
                HttpContext.Session.SetString(LOGGED_IN_FIRSTNAME, userVM.registerVM.FirstName);
                return RedirectToAction("Success");
            }
            // model did not validate correctly --> show errors to user
            TempData["errors"] = true;
            return RedirectToAction("Index");
        }

        // GET: successful registration
        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }
            UserRegViewModel NewUser = JsonConvert.DeserializeObject<UserRegViewModel>((string)TempData["user"]);
            ViewBag.User = NewUser;
            return View();
        }

        // GET: successful login
        [HttpGet]
        [Route("loginsuccess")]
        public IActionResult LoginSuccess()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
