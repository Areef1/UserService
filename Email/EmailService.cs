
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;
namespace SDSI.Access.Email
{
    public class EmailService
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "msareef5355@gmail.com"; // Replace with your Gmail address  
        private const string Password = "bojm zhth pipl pkrw"; // Replace with your Gmail password or App Password  

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                using (var smtpClient = new SmtpClient(SmtpServer, SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(SenderEmail, Password);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(SenderEmail),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true, // Set to true if you're sending HTML content  
                    };
                    mailMessage.To.Add(toEmail);

                    // Send the email asynchronously  
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed  
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}

//"EmailSettings": {
//    "SmtpServer": "smtp.your-email-provider.com",
//    "SmtpPort": 587,
//    "SenderName": "YourAppName",
//    "SenderEmail": "your-email@your-domain.com",
//    "Username": "your-email@your-domain.com",
//    "Password": "your-email-password"
//  }
//"EmailSettings": {
//"SmtpServer": "smtp.gmail.com",
//    "SmtpPort": 587,
//    "SenderName": "YourAppName",
//    "SenderEmail": "msareef5355@gmail.com",
//    "Username": "msareef5355@gmail.com",
//    "Password": "your-email-password"
//  }