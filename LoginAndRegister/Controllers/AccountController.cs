using LoginAndRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegister.Controllers
{
    public class AccountController : Controller
    {
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
                // Burada kullanıcı doğrulama işlemini yapın.
                // Örneğin, veritabanından kullanıcıyı kontrol edin.
                bool isValidUser = (model.Email == "john.doe@gmail.com" && model.Password == "password123");

                if (isValidUser)
                {
                    // Giriş başarılı, kullanıcıyı yönlendirin.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            // Giriş başarısızsa, aynı sayfayı tekrar gösterin.
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
            if (ModelState.IsValid)
            {
                // Burada kullanıcı kaydetme işlemini yapın.
                // Örneğin, veritabanına kaydedebilirsiniz.

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }

}
