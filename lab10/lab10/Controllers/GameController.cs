using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private static int drawnNumber;
        private static int n;
        private static Random rand = new Random();
        private static Boolean nCorrectSelection = false;
        private static Boolean drawn = false;

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Set(int n)
        {
            if (n <= 0)
            {
                nCorrectSelection = false;
                ViewBag.errorMsg = "Zakres musi byc liczba wieksza od 0. Sprobuj ponownie.";
            }
            else
            {
                ViewBag.errorMsg = "";
                nCorrectSelection = true;
                GameController.n = n;
                ViewBag.n = n;
            }

            drawn = false;
            return View("Set");
        }

        public IActionResult Draw()
        {
            if (nCorrectSelection)
            {
                ViewBag.errorMsg = "";
                drawnNumber = rand.Next(n);
                ViewBag.n = n - 1;
                drawn = true;
            }
            else
            {
                drawn = false;
                ViewBag.errorMsg = "Najpierw ustal poprawny zakres.";
            }

            return View("Draw");
        }

        public IActionResult Guess(int guessedNumber)
        {
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
                if (guessedNumber < drawnNumber) ViewBag.GuessingResult = GuessingResult.TooSmall;
                else if (guessedNumber > drawnNumber) ViewBag.GuessingResult = GuessingResult.TooLarge;
                else
                {
                    ViewBag.GuessingResult = GuessingResult.Perfect;
                    drawn = false;
                }
            }

            return View("Guess");
        }
    }
}
