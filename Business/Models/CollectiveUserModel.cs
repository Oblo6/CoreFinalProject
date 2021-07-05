using AppCore.Records;
using DataAccess.EntityFramework.Entities;
using System.Collections.Generic;

namespace Business.Models
{
    public class CollectiveUserModel : Record
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int CollectiveId { get; set; }

        public Collective Collective { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
