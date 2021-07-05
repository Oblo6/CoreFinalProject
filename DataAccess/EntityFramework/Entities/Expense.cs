using AppCore.Records;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.EntityFramework.Entities
{
    public class Expense : Record
    {
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public double Cost { get; set; }

        public DateTime PayDate { get; set; }

        public int CollectiveUserId { get; set; }

        public CollectiveUser CollectiveUser { get; set; }
    }
}
