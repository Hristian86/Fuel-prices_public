using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AkciqApp.ViewModels.ContactViewMOdel
{
    public class Contact
    {
        [Display(Name = "Name")]
        [Required]
        //[RegularExpression(@"\b([A-Z][a-z]+\d+)|([a-z]+\d+)|([A-Z]+[a-z]+ [A-Z]+[a-z]+)|([A-Z]+ [A-Z]+)|([a-z]+ [a-z]+)|([a-z]+)|([a-z]+ [a-z]+)|([A-Z]{1}[a-z]+\d+)|([a-z]+[0-9]+)|([a-z]+ )|([A-Z]+[a-z]+)|([A-Z]+)", ErrorMessage = "Invalid symbols")]
        [StringLength(50, ErrorMessage = "Minimum 2 symbols and max 50", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Subject")]
        [Required]
        [StringLength(50, ErrorMessage = "Minimum 5 symbols and max 50", MinimumLength = 5)]
        public string Subject { get; set; }

        [Display(Name = "Email address")]
        [Required]
        [RegularExpression(@"^([\w\.\-]{3,30})@([\w\-]{3,15})((\.(\w){2,3})+)$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Display(Name = "Message content")]
        [Required]
        [StringLength(500, ErrorMessage = "Minimum length of message content must be at least 10 symbols and max length is max 500 symbols", MinimumLength = 10)]
        public string Content { get; set; }

        public string ErrorMessage { get; set; }
    }
}
