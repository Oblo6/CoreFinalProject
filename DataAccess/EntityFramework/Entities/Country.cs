using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.EntityFramework.Entities
{
    public class Country : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<City> Cities { get; set; }

        public List<UserDetail> UserDetails { get; set; }
    }
}
