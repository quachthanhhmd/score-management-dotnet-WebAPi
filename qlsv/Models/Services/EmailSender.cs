using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using qlsv.Models.Interfaces;

namespace qlsv.Models.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string toEmail, string subject, string content)
        {


            var fromEmailAddress = _configuration["SendMail:FromEmailAddress"];
            var fromEmailDisplayName = _configuration["SendMail:FromEmailDisplayName"];
            var fromEMailPassWord = _configuration["SendMail:FromEmailPassword"];
            var smtpHost = _configuration["SendMail:SMTPHost"];
            var smtpPort = _configuration["SendMail:SMTPPost"];

            bool enableSsl = bool.Parse(_configuration["SendMail:EnabledSLL"]);

            string body = content;

            MailMessage mgs = new MailMessage(fromEmailAddress, toEmail, subject, content);

            mgs.Subject = subject;
            mgs.IsBodyHtml = true;
            mgs.Body = body;
            mgs.BodyEncoding = System.Text.Encoding.UTF8;


            //gen client
            var client = new SmtpClient(smtpHost, !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0);
            //client.Host = smtpHost;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            //client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEMailPassWord);
            client.Send(mgs);

        }
    }
}
