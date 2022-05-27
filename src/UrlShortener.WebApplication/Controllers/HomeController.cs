using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult TechTaskDetails()
        {
            return View("TechTaskDetails");
        }
    }
}