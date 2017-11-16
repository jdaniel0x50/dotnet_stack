using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using WeddingPlanner.Models;
using WeddingPlanner.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /weddings
        //  /weddings/add
        //  /weddings/create
        //  /weddings/{wedding_id}
        //  /weddings/{wedding_id}/rsvp
        //  /weddings/{wedding_id}/unrsvp
        //  /weddings/{wedding_id}/delete
        // ########## ROUTES ##########

        private const string LOGGED_IN_ID = "LoggedIn_Id";
        private const string LOGGED_IN_USERNAME = "LoggedIn_Username";
        private const string LOGGED_IN_FIRSTNAME = "LoggedIn_FirstName";

        // Entity PostGres Code First connection
        private WeddingPlannerContext _context;

        public WeddingController(WeddingPlannerContext context)
        {
            // Entity Framework connections
            _context = context;
        }

        // GET: /weddings/
        [HttpGet]
        [Route("weddings")]
        public IActionResult AllWeddings()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Wedding> ReturnedWeddings = _context.Weddings.OrderBy(item => item.WeddingDate).ToList();
            List<WeddingGuest> ReturnedGuests = _context.WeddingGuests.OrderBy(item => item.WeddingId).ToList();
            User CurrentUser = _context.Users.FirstOrDefault(item => item.UserId == HttpContext.Session.GetInt32(LOGGED_IN_ID));
            AllWeddingsListViewModel AllWeddings = new AllWeddingsListViewModel(ReturnedWeddings, CurrentUser, ReturnedGuests);
            ViewBag.Weddings = AllWeddings.WeddingLines;
            return View();
        }

        // GET: /weddings/add
        [HttpGet]
        [Route("weddings/add")]
        [ImportModelState]
        public IActionResult NewWedding()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["wedding_errors"] != null)
            {
                ViewBag.wedding_errors = ModelState;
            }
            return View();
        }

        // POST: /weddings/create
        [HttpPost]
        [Route("weddings/create")]
        [ExportModelState]
        public IActionResult CreateWedding(WeddingViewModel weddingVM)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TryValidateModel(weddingVM))
            {
                // Entity PostGres Code First command
                User CurrentUser = _context.Users.FirstOrDefault(item => item.UserId == HttpContext.Session.GetInt32(LOGGED_IN_ID));
                Wedding NewWedding = new Wedding(weddingVM, CurrentUser);
                _context.Weddings.Add(NewWedding);
                _context.SaveChanges();
                return RedirectToAction("AllWeddings");
            }
            // if view model was not valid, return to the add wedding page with errors in ModelState
            TempData["wedding_errors"] = true;
            return RedirectToAction("NewWedding");
        }

        //  /weddings/{wedding_id}/rsvp
        [HttpGet]
        [Route("weddings/{wedding_id}/rsvp")]
        public IActionResult RsvpWedding(int wedding_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            WeddingGuest newGuest = new WeddingGuest();
            newGuest.WeddingId = wedding_id;
            newGuest.UserId = (int)HttpContext.Session.GetInt32(LOGGED_IN_ID);
            _context.WeddingGuests.Add(newGuest);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }

        //  /weddings/{wedding_id}/unrsvp
        [HttpGet]
        [Route("weddings/{wedding_id}/unrsvp")]
        public IActionResult UnRsvpWedding(int wedding_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            WeddingGuest getGuestEntity = _context.WeddingGuests.SingleOrDefault(item => item.UserId == (int)HttpContext.Session.GetInt32(LOGGED_IN_ID) && item.WeddingId == wedding_id);
            _context.WeddingGuests.Remove(getGuestEntity);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }

        //  /weddings/{wedding_id}/delete
        [HttpGet]
        [Route("weddings/{wedding_id}/delete")]
        public IActionResult DeleteWedding(int wedding_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // confirm user is the wedding creator
            Wedding _wedding = _context.Weddings.FirstOrDefault(item => item.WeddingId == wedding_id);
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) != _wedding.CreatedById)
            {
                // the user did not create the wedding
                return RedirectToAction("AllWeddings");
            }

            _context.Weddings.Remove(_wedding);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }

        //  /weddings/{wedding_id}
        [HttpGet]
        [Route("weddings/{wedding_id}")]
        public IActionResult SingleWedding(int wedding_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Wedding _wedding = _context.Weddings.FirstOrDefault(item => item.WeddingId == wedding_id);
            User _createdUser = _context.Users.FirstOrDefault(item => item.UserId == _wedding.CreatedById);
            List<WeddingGuest> ReturnedGuests = _context.WeddingGuests.Where(item => item.WeddingId == _wedding.WeddingId).Include(item => item.Guest).OrderBy(item => item.Guest.FirstName).ToList();
            string MapAddress = _wedding.Address.Replace(" ", "%20");

            ViewBag.Wedding = _wedding;
            ViewBag.MapAddress = MapAddress;
            ViewBag.CreatedUser = _createdUser;
            ViewBag.Guests = ReturnedGuests;
            return View();
        }


    }
}
