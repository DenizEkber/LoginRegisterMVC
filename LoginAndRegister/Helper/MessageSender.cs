using System.Net;
using System.Net.Mail;

namespace LoginAndRegister.Helper
{
    public class MessageSender
    {
        private const string Host = "smtp.gmail.com";
        private const int Port = 587;
        private const string Username = "YourEmail";//Change this code
        private const string Password = "YourAppCode";// Change this code


        private static MessageSender instance;

        private MessageSender() { }

        public static MessageSender GetInstance()
        {
            if (instance == null)
            {
                instance = new MessageSender();
            }
            return instance;
        }

        public bool SendVerification(string email, string verificationCode)
        {
            using (var client = new SmtpClient(Host, Port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Username, Password);
                client.EnableSsl = true;

                var message = new MailMessage(Username, email)
                {
                    Subject = "Verfication code",
                    Body = $"Hello young,\n\nVerfication code: {verificationCode}"
                };

                try
                {
                    client.Send(message);
                    Console.WriteLine("Message are Sent.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while sending message");
                    return false;
                }

            }
        }
    }
}
