using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;

namespace Tools
{
    public class MailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public interface IMailSender
    {
        void Send(string[] To, string Subject, string Message);
    }
    public class GmailSender : IMailSender
    {
        private MailConfiguration mailConfiguration;
        public GmailSender(MailConfiguration mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;
        }

        public void Send(string[] To, string Subject, string HtmlMessage)
        {
            var to = new List<MailboxAddress>();
            to.AddRange(To.Select(x => new MailboxAddress(x)));
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(this.mailConfiguration.From));
            emailMessage.To.AddRange(to);
            emailMessage.Subject = Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = HtmlMessage;
            bodyBuilder.TextBody = "message prueba";
            emailMessage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(this.mailConfiguration.SmtpServer, this.mailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(this.mailConfiguration.UserName, this.mailConfiguration.Password);

                    client.Send(emailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
