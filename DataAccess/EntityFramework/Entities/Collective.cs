using AppCore.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.EntityFramework.Entities
{
    public class Collective : Record
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public List<CollectiveUser> CollectiveUsers { get; set; }
    }
}
