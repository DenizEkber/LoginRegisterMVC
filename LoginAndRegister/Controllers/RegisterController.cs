using DatabaseEntity.CodeFirst.Context;
using DatabaseEntity.CodeFirst.Entity.UserData;
using LoginAndRegister.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using LoginAndRegister.Helper;

namespace LoginAndRegister.Controllers
{
    public class RegisterController : Controller
    {
        ConvertHash convertHash; 

        public RegisterController()
        {
            convertHash = new ConvertHash();
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
                if (model.Agree)
                {
                    using (SiteContext ctx = new SiteContext())
                    {
                        bool emailExists =ctx.Users.Any(u=>u.Email == model.Email);
                        if (emailExists)
                        {
                            ModelState.AddModelError("Email", "This email address is already in use.");
                        }
                        else
                        {
                            (string passwordHash, string passwordSalt) = convertHash.HashPassword(model.Password);
                            var user = new Users
                            {
                                Name = model.FirstName + model.LastName,
                                Email = model.Email,
                                Password = passwordHash,
                                PasswordSalt = passwordSalt,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            };
                            ctx.Add(user);
                            ctx.SaveChanges();

                            var userDetail = new UserDetail
                            {
                                Id_User = user.Id,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                PhoneNumber = model.Phone,
                                PhotoData = new byte[0],
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            };
                            ctx.Add(userDetail);
                            ctx.SaveChanges();

                            return RedirectToAction("Login", "Login");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }


            }

            return View(model);
        }
        /*private (string, string) HashPassword(string password)
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
        }*/
    }
}
