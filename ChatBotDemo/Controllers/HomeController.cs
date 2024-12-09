using ChatBotDemo.Data;
using ChatBotDemo.Models;
using ChatBotDemo.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatBotDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _dbcontext;
        public HomeController(ILogger<HomeController> logger, MyDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {

            ViewData[SessionConstants.SESSION_ID] = HttpContext.Session.GetString(SessionConstants.SESSION_ID);
            return View();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return new JsonResult(_dbcontext?.Products?.ToList());
        }

        public IActionResult Privacy()
        {
            // Pass session ID to view using ViewData or ViewBag
            ViewData[SessionConstants.SESSION_ID] = HttpContext.Session.GetString(SessionConstants.SESSION_ID);
            
            return View();
        }

        public IActionResult ThrowError()
        {
            // Pass session ID to view using ViewData or ViewBag
            //ViewData[SessionConstants.SESSION_ID] = HttpContext.Session.GetString(SessionConstants.SESSION_ID);
            throw new Exception("usmanError: Privacy Method is not implemented.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = "Usman 565"
            });
        }
    }
}
