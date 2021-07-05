using System.ComponentModel;

namespace Business.Models.Filters
{
    public class ExpenseReportFilterModel
    {
        public int? CollectiveId { get; set; }

        public int? UserId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Expense Description")]
        public string ExpenseDescription { get; set; }

        [DisplayName("Cost")]
        public string CostBeginText { get; set; }

        public string CostEndText { get; set; }

        [DisplayName("Pay Date")]
        public string PayDateBeginText { get; set; }

        public string PayDateEndText { get; set; }

    }
}
