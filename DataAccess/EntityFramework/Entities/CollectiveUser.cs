using AppCore.Records;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Entities
{
    public class CollectiveUser : Record
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int CollectiveId { get; set; }

        public Collective Collective { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
