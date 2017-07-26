using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logger.Services;
using System.Threading;
using ExampleLogginDI.Utils;

namespace ExampleLogginDI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILoggerInterface _log;
        LoggerBlobStorage logger;

        public HomeController(ILoggerInterface log)
        {
            _log = log;
            logger = new LoggerBlobStorage(log);
        }


        public IActionResult Index()
        {
            ViewBag.Message = "Haciendo prueba de log";
            logger.InfoAsync("Info Log Test 1", GetType());
            logger.InfoAsync("Info Log Test 2", GetType());
            logger.InfoAsync("Info Log Test 3", GetType());
            logger.InfoAsync("Info Log Test 4", GetType());
            logger.InfoAsync("Info Log Test 5", GetType());
            logger.InfoAsync("Info Log Test 6", GetType());
            //_log.InfoAsync("Info Log test 1", GetType());
            //_log.InfoAsync("Info Log test 2", GetType());
            //_log.InfoAsync("Info Log test 3", GetType());
            //_log.InfoAsync("Info Log test 4", GetType());
            //_log.InfoAsync("Info Log test 5", GetType());
            //_log.InfoAsync("Info Log test 6", GetType());

            //Task.Factory.StartNew(() => {
            //for (int i = 1; i <= 10; i++)
            //{
            //    _log.InfoAsync("Info Log " + i, GetType());
            //}

            //});
            //_log.DebugAsync("Debug log", GetType());
            //_log.ErrorAsync(new Exception("Error Log"), GetType());
            //_log.WarningAsync("Warning Log", GetType());
            //_log.FatalAsync(new Exception("Fatal Log"), GetType());
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
