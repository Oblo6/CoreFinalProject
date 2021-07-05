using AppCore.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Entities
{
    public class UserDetail : Record
    {
        [Required]
        [StringLength(100)]
        public string EMail { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        public User User { get; set; }
    }
}
