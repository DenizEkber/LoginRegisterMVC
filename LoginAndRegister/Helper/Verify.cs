namespace LoginAndRegister.Helper
{
    public class Verify
    {
        public string verifyCode()
        {
            return GenerateVerificationCode();
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int code=1000+random.Next(9000);
            return code.ToString();
        }
    }
}
