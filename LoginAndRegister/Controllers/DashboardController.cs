using DatabaseEntity.CodeFirst.Context;
using LoginAndRegister.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LoginAndRegister.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserRepository _userRepository;

        public DashboardController()
        {
            _userRepository = new UserRepository(new SiteContext());
        }

        public IActionResult Dashboard()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userProfile = _userRepository.GetUserByEmail(email);
            if (userProfile == null)
            {
                // Kullanıcı bulunamazsa bir hata mesajı veya başka bir işlem ekleyebilirsiniz
                return RedirectToAction("Login", "Account");
            }
            return View(userProfile);
        }



        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(profilePicture.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)profilePicture.Length);
                }

                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var userProfile = _userRepository.GetUserByEmail(email);
                _userRepository.UpdateUserProfilePicture(userProfile.Id_User, imageData);
            }

            return RedirectToAction("Dashboard","Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
