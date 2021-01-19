namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AkciqApp.Constants;
    using AkciqApp.Models;
    using AkciqApp.Services.EmailService;
    using AkciqApp.ViewModels.ContactViewMOdel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService emailService;

        public ContactController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<string>> SendEmail(Contact contact)
        {
            try
            {
                var contents = "From : " + contact.Name + " . <br/>" + "Email : " + contact.Email + "<br/>" + "message" + "<br/>" + contact.Content;
                this.emailService.SendForgottenPass(WebConstantsVariables.supportEmail, contact.Subject, contents);

                return this.Ok(new Message(200, "created"));
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, new Message(500, new List<string>() { ex.Message }));
            }
        }
    }
}