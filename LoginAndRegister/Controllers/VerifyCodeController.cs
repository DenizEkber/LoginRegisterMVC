using LoginAndRegister.Helper;
using LoginAndRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegister.Controllers
{
    public class VerifyCodeController : Controller
    {
        SetPasswordViewModel _setPasswordViewModel; 
        public VerifyCodeController() { 

            _setPasswordViewModel =new SetPasswordViewModel();
        }

        [HttpGet]
        public ActionResult VerifyCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyCode(VerifyCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (TransferData.VerifyCode.TryGetValue(TransferData.SendEmail, out string correctCode) && correctCode.Equals(model.EnteredCode))
                {
                    //_setPasswordViewModel.Email = model.SendEmail;
                    return RedirectToAction("SetPassword", "SetPassword");
                }
                else
                {
                    ModelState.AddModelError("EnteredCode", "This code is not correctly");
                }
            }
            return View();
        }
    }
}
