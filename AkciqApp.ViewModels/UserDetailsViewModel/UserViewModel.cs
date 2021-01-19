using System;
using System.Collections.Generic;
using System.Text;
using AkciqApp.Mapping;
using AkciqApp.Models.Models;

namespace AkciqApp.ViewModels.UserDetailsViewModel
{
    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
