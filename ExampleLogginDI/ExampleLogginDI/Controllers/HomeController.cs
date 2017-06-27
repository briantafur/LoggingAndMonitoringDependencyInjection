using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AppLogger.Service;

namespace ExampleLogginDI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger _log;

        public HomeController(ILogger log)
        {
            _log = log;
            Console.WriteLine("Final del constructor");
            
        }


        public IActionResult Index()
        {
            ViewBag.Message = _log.Log("nada");
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
