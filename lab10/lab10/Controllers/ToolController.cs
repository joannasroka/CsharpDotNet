using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Controllers
{
    public class ToolController : Controller
    {
        [Route("Tool/Solve/{a}/{b}/{c}")]
        [Route("Tool/Solve/{a}")]
        [Route("Tool/Solve/{a}/{b}")]
        public IActionResult Solve(double a, double b = 0, double c = 0)
        {
            StringBuilder equation = new StringBuilder();

            if (a != 0)
            {
                equation.Append($"{a}x^2");
                if (b != 0) equation.Append(b > 0 ? $"+{b}x" : $"{b}x");
                if (c != 0) equation.Append(c > 0 ? $"+{c}" : $"{c}");


                double delta = b * b - 4 * a * c;
                if (delta > 0)
                {
                    double sqrtDelta = Math.Sqrt(delta);
                    double x1 = (-b - sqrtDelta) / (2 * a);
                    double x2 = (-b + sqrtDelta) / (2 * a);
                    ViewBag.NumberOfSolutions = 2;
                    ViewBag.X1 = $"{x1:G5}";
                    ViewBag.X2 = $"{x2:G5}";
                }
                else if (delta == 0)
                {
                    double x = (-1) * b / (2 * a);
                    ViewBag.NumberOfSolutions = 1;
                    ViewBag.X1 = $"{x:G5}";
                }
                else
                {
                    ViewBag.NumberOfSolutions = 0;
                }
            }
            else
            {
                if (b != 0)
                {
                    equation.Append($"{b}x");
                    if (c != 0) equation.Append(c > 0 ? $"+{c}" : $"{c}");
                    double x = (-c) / b;
                    ViewBag.NumberOfSolutions = 1;
                    ViewBag.X1 = $"{x:G5}";
                }
                else if (c == 0)
                {
                    equation.Append("0");
                    ViewBag.NumberOfSolutions = "Real numbers";
                }
                else
                {
                    equation.Append($"{c}");
                    ViewBag.NumberOfSolutions = 0;
                }

            }
            equation.Append(" = 0");
            ViewBag.Equation = equation.ToString();
            return View("Solve");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
