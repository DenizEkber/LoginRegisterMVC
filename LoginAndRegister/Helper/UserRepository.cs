using DatabaseEntity.CodeFirst.Context;
using DatabaseEntity.CodeFirst.Entity.UserData;
using LoginAndRegister.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAndRegister.Helper
{
    public class UserRepository
    {
        private readonly SiteContext _context;

        public UserRepository(SiteContext contex)
        {
            _context = contex;
        }

        public UserProfileViewModel GetUserByEmail(string userEmail)
        {
            var user = (from u in _context.Users
                        join ud in _context.UserDetails on u.Id equals ud.Id_User
                        where u.Email == userEmail
                        select new
                        {
                            u.Id,
                            u.Name,
                            u.Email,
                            ud.Id_User,
                            ud.PhoneNumber,
                            ud.PhotoData,
                            ud.FirstName,
                            ud.LastName,
                            u.UpdatedDate,
                            u.CreatedDate,
                        }).FirstOrDefault();
            if (user == null)
                return null;

            return new UserProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Id_User = user.Id_User,
                PhoneNumber = user.PhoneNumber,
                PhotoData = user.PhotoData,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UpdatedDate = user.UpdatedDate,
                CreatedDate = user.CreatedDate,
            };
        }

        public void UpdateUserProfilePicture(int userId, byte[] PhotoData)
        {
            var userDetail = _context.UserDetails.SingleOrDefault(u => u.Id_User == userId);
            if (userDetail != null)
            {
                userDetail.PhotoData = PhotoData;
                _context.UserDetails.Update(userDetail);
                _context.SaveChanges();
            }
        }
    }
}
