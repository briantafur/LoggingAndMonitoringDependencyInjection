using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logger.Services;
using System.Threading;

namespace ExampleLogginDI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILoggerInterface _log;

        public HomeController(ILoggerInterface log)
        {
            _log = log;
        }


        public IActionResult Index()
        {
            ViewBag.Message = "Haciendo prueba de log";
            for (int i = 1; i <= 5; i++)
            {
                _log.InfoAsync("Info Log " + i, GetType());
            }
            _log.DebugAsync("Debug log", GetType());
            _log.ErrorAsync(new Exception("Error Log"), GetType());
            _log.WarningAsync("Warning Log", GetType());
            _log.FatalAsync(new Exception("Fatal Log"), GetType());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";


            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
