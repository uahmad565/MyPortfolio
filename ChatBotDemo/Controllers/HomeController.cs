using ChatBotDemo.Models;
using ChatBotDemo.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatBotDemo.Controllers
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

            ViewData[SessionConstants.SESSION_ID] = HttpContext.Session.GetString(SessionConstants.SESSION_ID);
            return View();
        }

        public IActionResult Privacy()
        {
            // Pass session ID to view using ViewData or ViewBag
            ViewData[SessionConstants.SESSION_ID] = HttpContext.Session.GetString(SessionConstants.SESSION_ID);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
