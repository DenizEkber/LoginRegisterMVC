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
        /*public IActionResult Login()
        {
            return View();
        }*/
        public IActionResult VerifyCode()
        {
            return View();
        }
        /*public IActionResult Register()
        {
            return View();
        }*/
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




        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Burada kullan?c? do?rulama i?lemini yap?n.
                // Örne?in, veritaban?ndan kullan?c?y? kontrol edin.
                bool isValidUser = (model.Email == "john.doe@gmail.com" && model.Password == "password123");

                if (isValidUser)
                {
                    // Giri? ba?ar?l?, kullan?c?y? yönlendirin.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            // Giri? ba?ar?s?zsa, ayn? sayfay? tekrar gösterin.
            return View(model);
        }




        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            Console.WriteLine(model.Agree);
            foreach (var modelState in ModelState)
            {
                var key = modelState.Key;
                var value = modelState.Value;
                if (value.Errors.Any())
                {
                    foreach (var error in value.Errors)
                    {
                        _logger.LogError($"ModelState Error: Key={key}, Error={error.ErrorMessage}");
                    }
                }
            }


            if (ModelState.IsValid)
            {
                if (model.Agree)
                {
                    using(SiteContext ctx =new SiteContext())
                    {
                        (string passwordHash, string passwordSalt) = HashPassword(model.Password);
                        var users = new Users
                        {
                            Name=model.FirstName+model.LastName,
                            Email=model.Email,
                            Password=passwordHash,
                            PasswordSalt=passwordSalt,
                            CreatedDate=DateTime.Now,
                            UpdatedDate=DateTime.Now,
                        };
                        ctx.Add(users);
                        ctx.SaveChanges();

                        var userDetail = new UserDetail
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            PhoneNumber = model.Phone,
                            PhotoData = new byte[0],
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        };
                        ctx.Add(userDetail);
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }


            }

            return View(model);
        }
        private (string, string) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                string salt = Convert.ToBase64String(saltBytes);

                using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    byte[] hashBytes = rfc2898DeriveBytes.GetBytes(20);
                    string hash = Convert.ToBase64String(hashBytes);
                    return (hash, salt);
                }
            }
        }
    }
}
