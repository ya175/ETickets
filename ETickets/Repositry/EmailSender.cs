using System.Net.Mail;
using System.Net;
using ETickets.IRepositry;

namespace ETickets.Repositry
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            var mail = "EL_Warsha@outlook.com";
            var pass = "";


            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pass)
            
            };

            return client.SendMailAsync(
                new MailMessage(from: "EL_Warsha@outlook.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}