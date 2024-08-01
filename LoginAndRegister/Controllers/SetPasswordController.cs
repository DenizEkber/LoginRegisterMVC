using DatabaseEntity.CodeFirst.Context;
using LoginAndRegister.Helper;
using LoginAndRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegister.Controllers
{
    public class SetPasswordController : Controller
    {

        ConvertHash convertHash; 

        SetPasswordController()
        {
            convertHash = new ConvertHash();
        }

        [HttpGet]
        public ActionResult SetPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SiteContext ctx = new SiteContext())
                {
                    var user = ctx.Users.FirstOrDefault(u=>u.Email == TransferData.SendEmail);
                    if (user != null)
                    {
                        (string passwordHash, string passwordSalt) = convertHash.HashPassword(model.CreatePassword);
                        user.Password = passwordHash;
                        user.PasswordSalt = passwordSalt;
                        ctx.Users.Update(user);
                        ctx.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                    
                }
            }
            return View();
        }
    }
}
