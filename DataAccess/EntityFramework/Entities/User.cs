using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.EntityFramework.Entities
{
    public class User : Record
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(16)]
        public string Password { get; set; }

        public bool Active { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int UserDetailId { get; set; }

        public UserDetail UserDetail { get; set; }

        public List<CollectiveUser> CollectiveUsers { get; set; }
    }
}
