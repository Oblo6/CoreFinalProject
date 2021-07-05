using Business.Models.Filters;
using Business.Models.Reports;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI.Models
{
    public class ExpensesReportAjaxIndexViewModel
    {
        public ExpensesReportAjaxIndexViewModel()
        {
            PageNumber = 1;
            OrderByDirectionAscending = true;
        }

        public List<ExpensesReportModel> ExpensesReport { get; set; }

        public ExpenseReportFilterModel ExpensesFilter { get; set; }

        public SelectList Collectives { get; set; }

        public int PageNumber { get; set; }

        public SelectList Pages { get; set; }

        public string OrderByExpression { get; set; }

        public bool OrderByDirectionAscending { get; set; }
    }
}
