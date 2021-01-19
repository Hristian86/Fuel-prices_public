using System;
using System.Collections.Generic;
using System.Text;

namespace AkciqApp.Services.EmailService
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);

        void SendForgottenPass(string to, string subject, string html);
    }
}
