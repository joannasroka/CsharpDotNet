using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Controllers
{
    public enum GuessingResult
    {
        TooSmall,
        Perfect,
        TooLarge
    }

    public class GameController : Controller
    {
        private int drawnNumber;
        private static Random rand = new Random();
        private Boolean nCorrectSelection = false;
        private Boolean drawn = false;

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Set(int n)
        {
            if (n <= 0)
            {
                HttpContext.Session.SetInt32("nCorrectSelection", 0);
                ViewBag.errorMsg = "Zakres musi byc liczba wieksza od 0. Sprobuj ponownie.";
            }
            else
            {
                HttpContext.Session.SetInt32("nCorrectSelection", 1);
                ViewBag.errorMsg = "";
                HttpContext.Session.SetInt32("n", n);
                ViewBag.n = n;
            }

            HttpContext.Session.SetInt32("drawn", 0);
            return View("Set");
        }

        public IActionResult Draw()
        {
            nCorrectSelection = Convert.ToBoolean(HttpContext.Session.GetInt32("nCorrectSelection"));
            if (nCorrectSelection)
            {
                ViewBag.errorMsg = "";
                drawnNumber = rand.Next((int)HttpContext.Session.GetInt32("n"));
                HttpContext.Session.SetInt32("drawnNumber", drawnNumber);
                ViewBag.n = HttpContext.Session.GetInt32("n") - 1;
                HttpContext.Session.SetInt32("drawn", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("drawn", 0);
                ViewBag.errorMsg = "Najpierw ustal poprawny zakres.";
            }

            return View("Draw");
        }

        public IActionResult Guess(int guessedNumber)
        {
            nCorrectSelection = Convert.ToBoolean(HttpContext.Session.GetInt32("nCorrectSelection"));
            drawn = Convert.ToBoolean(HttpContext.Session.GetInt32("drawn"));
            if (!nCorrectSelection)
            {
                ViewBag.errorMsg = "Najpierw ustal poprawny zakres.";
            }
            else if (!drawn)
            {
                ViewBag.errorMsg = "Najpierw wylosuj liczbe.";
            }
            else
            {
                ViewBag.GuessedNumber = guessedNumber;
                ViewBag.errorMsg = "";
                drawnNumber = (int)HttpContext.Session.GetInt32("drawnNumber");
                if (guessedNumber < drawnNumber) ViewBag.GuessingResult = GuessingResult.TooSmall;
                else if (guessedNumber > drawnNumber) ViewBag.GuessingResult = GuessingResult.TooLarge;
                else
                {
                    ViewBag.GuessingResult = GuessingResult.Perfect;
                    HttpContext.Session.SetInt32("drawn", 0);
                }
            }

            return View("Guess");
        }



    }
}
