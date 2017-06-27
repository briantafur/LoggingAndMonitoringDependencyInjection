using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ExampleLogginDI.Services;

namespace ExampleLogginDI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMonsterService _monsterService;

        public HomeController(IMonsterService monsterService)
        {
            _monsterService = monsterService;
            _monsterService.Log("Haciendo una prueba del log....");
            Console.WriteLine("Final del constructor");
            
        }


        public IActionResult Index()
        {
            ViewBag.Message = _monsterService.Log("nada");
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
