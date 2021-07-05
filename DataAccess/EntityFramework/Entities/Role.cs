using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.EntityFramework.Entities
{
    public class Role : Record
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
