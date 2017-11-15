using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using restauranter.Models;
using restauranter.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace restauranter.Controllers
{
    public class ReviewController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /reviews
        //  /reviews/add
        //  /reviews/create
        //  /restaurants
        //  /restaurants/create
        // ########## ROUTES ##########

        private const string LOGGED_IN_ID = "LoggedIn_Id";
        private const string LOGGED_IN_USERNAME = "LoggedIn_Username";
        private const string LOGGED_IN_FIRSTNAME = "LoggedIn_FirstName";

        // Entity PostGres Code First connection
        private RestauranterContext _context;

        public ReviewController(RestauranterContext context)
        {
            // Entity Framework connections
            _context = context;
        }

        // GET: /reviews/
        [HttpGet]
        [Route("reviews")]
        public IActionResult AllReviews()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Review> ReturnedReviews = _context.Reviews.Include(rev => rev.User).Include(rev => rev.Restaurant).OrderByDescending(rev => rev.CreatedAt).ToList();
            ViewBag.Reviews = ReturnedReviews;
            return View();
        }

        // GET: /reviews/add
        [HttpGet]
        [Route("reviews/add")]
        [ImportModelState]
        public IActionResult NewReview()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["review_errors"] != null)
            {
                ViewBag.review_errors = ModelState;
            }
            List<Restaurant> ReturnedRestaurants = _context.Restaurants.OrderBy(rest => rest.Name).ToList();
            ViewBag.Restaurants = ReturnedRestaurants;
            return View();
        }

        private void AddRestaurantError()
        {
            // the restaurant was not found in the database
            string key = "Restaurant";
            string errorMessage = "The restaurant was not found. Please create a restaurant record first, and try again.";
            ModelState.AddModelError(key, errorMessage);
            return;
        }

        // POST: /reviews/create
        [HttpPost]
        [Route("reviews/create")]
        [ExportModelState]
        public IActionResult CreateReview(ReviewViewModel reviewVM)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TryValidateModel(reviewVM))
            {
                // Entity PostGres Code First command
                // retrieve logged in user entity
                User logged_in_user = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32(LOGGED_IN_ID));
                // retrieve selected restaurant entity
                // if the restaurant name is not in the database, return an error
                Restaurant selected_restaurant = new Restaurant();
                try
                {
                    selected_restaurant = _context.Restaurants.SingleOrDefault(rest => rest.Name == reviewVM.Restaurant);
                    if (selected_restaurant == null)
                    {
                        AddRestaurantError();
                        TempData["review_errors"] = true;
                        return RedirectToAction("NewReview");
                    }
                }
                catch (Exception ex)
                {
                    AddRestaurantError();
                    TempData["review_errors"] = true;
                    return RedirectToAction("NewReview");
                }

                // if found restaurant, add the review to the database
                Review NewReview = new Review(reviewVM, logged_in_user, selected_restaurant);
                _context.Reviews.Add(NewReview);
                _context.SaveChanges();
                return RedirectToAction("AllReviews");
            }
            // if view model was not valid, return to the add review page with errors in modelstate
            TempData["review_errors"] = true;
            return RedirectToAction("NewReview");        }

        // GET: /restaurants
        [HttpGet]
        [Route("restaurants")]
        [ImportModelState]
        public IActionResult AllRestaurants()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["errors"] != null)
            {
                ViewBag.errors = ModelState;
            }
            List<Restaurant> ReturnedRestaurants = _context.Restaurants.OrderBy(rest => rest.Name).ToList();
            ViewBag.Restaurants = ReturnedRestaurants;
            return View();
        }

        private void AddRestaurantNameError()
        {
            // the restaurant already has a record with the same name in the database
            string key = "Restaurant";
            string errorMessage = "This restaurant name has already been selected. Please select this restaurant, or use a new name.";
            ModelState.AddModelError(key, errorMessage);
            return;
        }

        // POST: /restaurants/create
        [HttpPost]
        [Route("restaurants/create")]
        [ExportModelState]
        public IActionResult CreateRestaurant(RestaurantViewModel restVM)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index");
            }

            Restaurant test_restaurant = new Restaurant();
            if (TryValidateModel(restVM))
            {
                // Restaurant View Model is valid
                // Confirm there are no other restaurants with the same name; if so return error
                try
                {
                    test_restaurant = _context.Restaurants.SingleOrDefault(rest => rest.Name == restVM.Name);
                    if (test_restaurant != null)
                    {
                        AddRestaurantNameError();
                        TempData["errors"] = true;
                        return RedirectToAction("AllRestaurants");
                    }
                }
                catch (Exception ex)
                { 
                    // if error, there were no restaurants of same name --> proceed
                }
            }
            else
            {
                // the model is not valid
                TempData["errors"] = true;
                return RedirectToAction("AllRestaurants");
            }
            Restaurant NewRestaurant = new Restaurant(restVM);
            _context.Restaurants.Add(NewRestaurant);
            _context.SaveChanges();
            return RedirectToAction("AllRestaurants");
        }
    }
}
