using System.Security.Cryptography;

namespace LoginAndRegister.Helper
{
    public class ConvertHash
    {
        public (string, string) HashPassword(string password)
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
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(enteredPassword, Convert.FromBase64String(storedSalt), 10000))
            {
                string hash = Convert.ToBase64String(rfc2898.GetBytes(20));
                return hash == storedHash;
            }
        }
    }
}
