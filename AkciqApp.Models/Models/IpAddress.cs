namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    public class IpAddress : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        [MaxLength(50)]
        public string Ip { get; set; }

        public string Email { get; set; }

        public virtual ApplicationUser User { get; set; }

        public long Visits { get; set; }

        public string Info { get; set; }
    }
}
