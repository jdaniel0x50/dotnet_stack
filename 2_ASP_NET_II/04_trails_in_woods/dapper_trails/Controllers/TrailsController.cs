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
    public class TrailsController : Controller
    {
        // ########## ROUTES ##########
        //  /trails
        //  /trails/create
        //  /trails/{trai_id}
        // ########## ROUTES ##########

        private const string LOGGED_IN_ID = "LoggedIn_Id";
        private const string LOGGED_IN_USERNAME = "LoggedIn_Username";
        private const string LOGGED_IN_FIRSTNAME = "LoggedIn_FirstName";
        private readonly TrailFactory trailFactory;
        private readonly DbConnector _dbConnector;

        public TrailsController(DbConnector connect)
        {
            _dbConnector = connect;
            trailFactory = new TrailFactory();
        }

        // GET: /trails/
        [HttpGet]
        [Route("/trails")]
        public IActionResult ViewAll()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.trails = trailFactory.FindAll();
            return View("alltrails");
        }

        // GET: /trails/add
        [HttpGet]
        [Route("/trails/add")]
        [ImportModelState]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }
            if (TempData["trailErrors"] != null)
            {
                ViewBag.trailErrors = ModelState;
            }
            return View("addtrail");
        }

        // POST: /trails/create
        [HttpPost]
        [Route("/trails/create")]
        [ExportModelState]
        public IActionResult Create(TrailAddViewModel newTrail)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }
            if (TryValidateModel(newTrail))
            {
                trailFactory.Add(newTrail);
                return RedirectToAction("ViewAll");
            }
            else
            {
                TempData["trailErrors"] = true;
                return RedirectToAction("Add");
            }
        }

        // GET: /trails/{trail_id}
        [HttpGet]
        [Route("/trails/{trail_id}")]
        [ImportModelState]
        public IActionResult SingleTrail(int trail_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.SingleTrail = trailFactory.FindByID(trail_id);
            return View("singletrail");
        }



    }
}
