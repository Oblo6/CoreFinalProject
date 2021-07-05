using System;
using System.ComponentModel;

namespace Business.Models.Reports
{
    public class ExpensesReportModel
    {
        public int CollectiveId { get; set; }

        [DisplayName("Collective")]
        public string CollectiveName { get; set; }

        [DisplayName("Expense Description")]
        public string ExpenseDescription { get; set; }

        public double Cost { get; set; }

        [DisplayName("Cost")]
        public string CostText { get; set; }

        public int UserId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Pay Date")]
        public string PayDateText { get; set; }

        public DateTime? PayDate { get; set; }
    }
}
