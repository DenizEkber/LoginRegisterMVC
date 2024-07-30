using DatabaseEntity.CodeFirst.Context;
using LoginAndRegister.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LoginAndRegister.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            /*if (User.Identity.IsAuthenticated)
            {
                // Kullanıcı zaten giriş yapmışsa, doğrudan ana sayfaya yönlendir.
                return RedirectToAction("Index", "Home");
            }*/
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SiteContext ctx = new SiteContext())
                {
                    var user = ctx.Users.SingleOrDefault(u => u.Email == model.Email);
                    if (user != null && VerifyPassword(model.Password, user.Password, user.PasswordSalt))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, model.Email)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe // Beni Hatırla seçeneğine göre çerezi kalıcı yap
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authProperties);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Hatalı giriş durumu
                        Console.WriteLine("Htalai password");
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }

            // Model geçersizse veya hata varsa, aynı sayfayı tekrar göster
            return View(model);
        }

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(enteredPassword, Convert.FromBase64String(storedSalt), 10000))
            {
                string hash = Convert.ToBase64String(rfc2898.GetBytes(20));
                return hash == storedHash;
            }
        }
    }
}
