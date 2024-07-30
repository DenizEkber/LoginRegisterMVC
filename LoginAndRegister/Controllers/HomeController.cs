using LoginAndRegister.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DatabaseEntity.CodeFirst.Context;
using DatabaseEntity.CodeFirst.Entity.UserData;
using System.Security.Cryptography;

namespace LoginAndRegister.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult SetPassword()
        {
            return View();
        }
        public IActionResult PasswordReset()
        {
            return View();
        }

        public IActionResult VerifyCode()
        {
            return View();
        }

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
