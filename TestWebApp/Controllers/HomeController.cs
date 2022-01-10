using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [ResponseCache.Attributes.ResponseCache("Home", 100)]

    public class HomeController : Controller
    {
        [ResponseCache.Attributes.ResponseCache("HomeIndex", 10)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}