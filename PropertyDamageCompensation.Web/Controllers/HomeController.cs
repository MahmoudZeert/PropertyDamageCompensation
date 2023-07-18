using Microsoft.AspNetCore.Mvc;
using PropertyDamageCompensation.Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PropertyDamageCompensation.Web.Controllers
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
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
                
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult MyError(string error)
        {
            var myErrorVM = JsonSerializer.Deserialize<MyErrorViewModel>(error);
            //var erroVM = new MyErrorViewModel
            //{
            //    StatusCode = error.StatusCode,
            //    ErrorMessage = error.ErrorMessage
            //};

            return View(myErrorVM);
        }
    }
}