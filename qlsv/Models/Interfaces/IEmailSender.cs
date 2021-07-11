using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IEmailSender
    {

        void SendMail(string toEmail, string subject, string content);
    }
}
