using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {

        private Dachi GetDachiObject()
        {
            Dachi objDachi;
            var serializedDachi = "";
            if (HttpContext.Session.GetString("dachi") == null)
            {
                objDachi = new Dachi();
                serializedDachi = JsonConvert.SerializeObject(objDachi);
                HttpContext.Session.SetString("dachi", serializedDachi);
                string message = "Your dojodachi is waiting for you!";
                HttpContext.Session.SetString("message", message);
                string imgSource = GetDachiImg("feed");
                HttpContext.Session.SetString("imgSource", imgSource);
            }
            else
            {
                serializedDachi = HttpContext.Session.GetString("dachi");
                objDachi = JsonConvert.DeserializeObject<Dachi>(serializedDachi);
            }
            return objDachi;
        }

        private void ReassignSessionJson(Dachi objDachi)
        {
            var serializedDachi = JsonConvert.SerializeObject(objDachi);
            HttpContext.Session.SetString("dachi", serializedDachi);
            return;
        }

        private int GetRandomInt(List<string> list)
        {
            Random rand = new Random();
            int indexVal = rand.Next(0, list.Count);
            return indexVal;
        }

        private string GetDachiImg(string method)
        {
            List<string> failImages = new List<string>();
            failImages.Add("/images/grumpy_hedgehog.jpg");
            failImages.Add("/images/hungry_hedgehog.jpg");

            List<string> feedImages = new List<string>();
            feedImages.Add("/images/happy_hedgehog.jpg");
            feedImages.Add("/images/happy2_hedgehog.jpeg");

            List<string> playImages = new List<string>();
            playImages.Add("/images/happy_hedgehog.jpg");
            playImages.Add("/images/playful_hedgehog.jpg");
            playImages.Add("/images/playful2_hedgehog.jpg");

            List<string> workImages = new List<string>();
            workImages.Add("/images/happy_hedgehog.jpg");
            workImages.Add("/images/sonic2_hedgehog.jpg");

            List<string> sleepImages = new List<string>();
            sleepImages.Add("/images/sleeping_hedgehog.jpg");

            List<string> lostImages = new List<string>();
            lostImages.Add("/images/sad_hedgehog.jpg");
            lostImages.Add("/images/grumpy_hedgehog.jpg");

            List<string> wonImages = new List<string>();
            wonImages.Add("/images/sonic_hedgehog.png");

            int imgIndex = 0;
            string imgSource = "";
            switch (method)
            {
                // method failed
                case "fail":
                    imgIndex = GetRandomInt(failImages);
                    imgSource = failImages[imgIndex];
                    break;
                
                // feed method
                case "feed":
                    imgIndex = GetRandomInt(feedImages);
                    imgSource = feedImages[imgIndex];
                    break;

                // play method
                case "play":
                    imgIndex = GetRandomInt(playImages);
                    imgSource = playImages[imgIndex];
                    break;

                // work method
                case "work":
                    imgIndex = GetRandomInt(workImages);
                    imgSource = workImages[imgIndex];
                    break;

                // sleep method
                case "sleep":
                    imgIndex = GetRandomInt(sleepImages);
                    imgSource = sleepImages[imgIndex];
                    break;

                // lose
                case "lose":
                    imgIndex = GetRandomInt(lostImages);
                    imgSource = lostImages[imgIndex];
                    break;

                // won
                case "won":
                    imgIndex = GetRandomInt(wonImages);
                    imgSource = wonImages[imgIndex];
                    break;
            }
            return imgSource;
        }


        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            Dachi objDachi = GetDachiObject();
            ViewBag.fullness = objDachi.fullness;
            ViewBag.happiness = objDachi.happiness;
            ViewBag.energy = objDachi.energy;
            ViewBag.meals = objDachi.meals;
            ViewBag.message = HttpContext.Session.GetString("message");
            ViewBag.imgSource = HttpContext.Session.GetString("imgSource");
            return View();
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            Dachi objDachi = GetDachiObject();
            int foodAte = objDachi.Feed();
            if (foodAte == -1)
            {
                string message = "Your dachi did not like your cooking, and rejected the food! (Fullness +0; Meals -1)";
                HttpContext.Session.SetString("message", message);
                HttpContext.Session.SetString("imgSource", GetDachiImg("fail"));
            }
            else
            {
                string message = "'Yum!' Your dachi thanks you for remembering to cook today. (Fullness +" + foodAte + "; Meals -1";
                HttpContext.Session.SetString("message", message);
                HttpContext.Session.SetString("imgSource", GetDachiImg("feed"));
            }
            ReassignSessionJson(objDachi);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            Dachi objDachi = GetDachiObject();
            int happinessGained = objDachi.Play();
            if (happinessGained == -1)
            {
                string message = "Your dachi did not like the game you chose, and promptly walked away! (Happiness +0; Energy -5)";
                HttpContext.Session.SetString("message", message);
                HttpContext.Session.SetString("imgSource", GetDachiImg("fail"));
            }
            else
            {
                string message = "'Fun!' Your dachi had a great time. (Happiness +" + happinessGained + "; Energy -5)";
                HttpContext.Session.SetString("message", message);
                HttpContext.Session.SetString("imgSource", GetDachiImg("play"));
            }
            ReassignSessionJson(objDachi);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            Dachi objDachi = GetDachiObject();
            int mealsGained = objDachi.Work();
            string message = "That was hard work! (Meals +" + mealsGained + "; Energy -5)";
            HttpContext.Session.SetString("message", message);
            HttpContext.Session.SetString("imgSource", GetDachiImg("work"));
            ReassignSessionJson(objDachi);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            Dachi objDachi = GetDachiObject();
            int energyGained = objDachi.Sleep();
            string message = "'Yawn'! That feels better. (Energy +" + energyGained + "; Fullness -5; Happiness -5)";
            HttpContext.Session.SetString("message", message);
            HttpContext.Session.SetString("imgSource", GetDachiImg("sleep"));
            ReassignSessionJson(objDachi);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
