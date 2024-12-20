using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
        public IActionResult GetKeyVaultSecret()
        {
            try
            {
                SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                     }
                };
                var client = new SecretClient(new Uri("https://usman-keyvault-eastus.vault.azure.net/"), new DefaultAzureCredential(), options);

                KeyVaultSecret secret = client.GetSecret("connStr");


                string secretValue = secret.Value;
                return Ok(secretValue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UrlOutput(string url)
        {
            //if (string.IsNullOrEmpty(url))
            //    return BadRequest("Url empty or null.");
            // URL to send the GET request to
            //string url = "http://usmandev.eastus.cloudapp.azure.com/";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    ViewBag.Response = response;
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                return new JsonResult(_dbcontext?.Products?.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
