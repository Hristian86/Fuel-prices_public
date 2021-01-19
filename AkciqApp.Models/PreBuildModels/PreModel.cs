namespace AkciqApp.Models.PreBuildModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class PreModel
    {
        [Key]
        public int id { get; set; }

        [MaxLength(100)]
        public string IpAddress { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedOn { get; set; }
    }
}