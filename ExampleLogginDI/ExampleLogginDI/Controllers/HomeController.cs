using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logger.Services;

namespace ExampleLogginDI.Controllers
{
    public class HomeController : Controller
    {

        private readonly LoggerInterface _log;
        //private readonly ILog _log;

        public HomeController(LoggerInterface log)
        {
              _log = log;
            //_log = LogManager.GetLogger(typeof(HomeController));
            //_log.Info("Testing logger");
            //Console.WriteLine("Final del constructor");
        }


        public IActionResult Index()
        {
            ViewBag.Message = "Haciendo prueba de log";
            _log.Info("Info Log", GetType());
            _log.Debug("Debug log", GetType());
            _log.Error(new Exception("Error Log"), GetType());
            _log.Warning("Warning Log", GetType());
            _log.Fatal(new Exception("Fatal Log"), GetType());
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
