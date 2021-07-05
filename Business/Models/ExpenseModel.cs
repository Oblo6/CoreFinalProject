using AppCore.Records;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ExpenseModel : Record
    {
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public double Cost { get; set; }

        [DisplayName("Pay Date")]
        public DateTime PayDate { get; set; }

        public int UserId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public int CollectiveUserId { get; set; }

        public CollectiveUserModel collectiveUser { get; set; }

        public int CollectiveId { get; set; }

        [DisplayName("Group Name")]
        public string CollectiveName { get; set; }
    }
}
