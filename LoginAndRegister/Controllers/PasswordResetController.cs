using DatabaseEntity.CodeFirst.Context;
using LoginAndRegister.Helper;
using LoginAndRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegister.Controllers
{
    public class PasswordResetController : Controller
    {
        MessageSender _messageSender;
        Verify verify;
        VerifyCodeViewModel _verifyCodeViewModel;

        private readonly Dictionary<string, string> verificationCodes; 

        public PasswordResetController()
        {
            _messageSender = MessageSender.GetInstance();

            verify = new Verify();

            _verifyCodeViewModel = new VerifyCodeViewModel();


            verificationCodes = new Dictionary<string, string>();
        }

        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PasswordReset(PasswordResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SiteContext ctx = new SiteContext())
                {
                    bool emailExists = ctx.Users.Any(u=>u.Email == model.Email);
                    if (emailExists)
                    {
                        string verificationCode = verify.verifyCode();

                        verificationCodes.Add(model.Email, verificationCode);

                        _messageSender.SendVerification(model.Email, verificationCode);


                        TransferData.VerifyCode = verificationCodes;
                        TransferData.SendEmail = model.Email;

                        return RedirectToAction("VerifyCode", "VerifyCode");

                    }
                    else
                    {
                        
                        ModelState.AddModelError("Email", "This email address is not already in use.");
                    }
                }
            }
            return View();
        }
    }
}
