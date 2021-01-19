namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Services.EmailService;
    using Microsoft.AspNetCore.Mvc;

    public class PersonalInfoController : Controller
    {
        public PersonalInfoController(IEmailService emailService)
        {
        }

        public IActionResult Contact() => this.View();
    }
}