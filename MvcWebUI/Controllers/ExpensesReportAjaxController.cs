using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using AppCore.Business.Models.Results;
using Business.Models.Filters;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcWebUI.Models;
using MvcWebUI.Settings;
using System;
using System.Collections.Generic;

namespace MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExpensesReportAjaxController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesReportAjaxController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public IActionResult Index(int? collectiveId)
        {
            var expensesFilter = new ExpenseReportFilterModel() { CollectiveId = collectiveId };
            var page = new PageModel() { RecordsPerPageCount = AppSettings.RecordsPerPageCount };
            var result = _expenseService.GetExpensesReport(expensesFilter, page);
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            var expensesReport = result.Data;

            #region Paging
            double recordsCount = page.RecordsCount;
            double recordsPageCount = page.RecordsPerPageCount;
            double totalPageCount = Math.Ceiling(recordsCount / recordsPageCount);
            List<SelectListItem> pageSelectListItems = new List<SelectListItem>();
            if (totalPageCount == 0)
                pageSelectListItems.Add(new SelectListItem() { Value = "1", Text = "1" });
            else
                for (int pageNumber = 1; pageNumber <= totalPageCount; pageNumber++)
                    pageSelectListItems.Add(new SelectListItem() { Value = pageNumber.ToString(), Text = pageNumber.ToString() });
            #endregion

            var viewModel = new ExpensesReportAjaxIndexViewModel()
            {
                ExpensesReport = expensesReport,
                ExpensesFilter = expensesFilter,
                Pages = new SelectList(pageSelectListItems, "Value", "Text")
            };

            ViewData["ExpensesReport"] = viewModel;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ExpensesReportAjaxIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var page = new PageModel()
                {
                    PageNumber = viewModel.PageNumber,
                    RecordsPerPageCount = AppSettings.RecordsPerPageCount
                };

                #region Ordering
                var order = new OrderModel()
                {
                    Expression = viewModel.OrderByExpression,
                    DirectionAscending = viewModel.OrderByDirectionAscending
                };
                #endregion

                var result = _expenseService.GetExpensesReport(viewModel.ExpensesFilter, page, order);
                if (result.Status == ResultStatus.Exception)
                    throw new Exception(result.Message);
                viewModel.ExpensesReport = result.Data;

                #region Paging
                double recordsCount = page.RecordsCount; // filtrelenmiş veya filtrelenmemiş toplam kayıt sayısı
                double recordsPerPageCount = page.RecordsPerPageCount; // sayfa başına kayıt sayısı
                double totalPageCount = Math.Ceiling(recordsCount / recordsPerPageCount); // toplam sayfa sayısı
                List<SelectListItem> pageSelectListItems = new List<SelectListItem>();
                if (totalPageCount == 0)
                {
                    pageSelectListItems.Add(new SelectListItem()
                    {
                        Value = "1",
                        Text = "1"
                    });
                }
                else
                {
                    for (int pageNumber = 1; pageNumber <= totalPageCount; pageNumber++)
                    {
                        pageSelectListItems.Add(new SelectListItem()
                        {
                            Value = pageNumber.ToString(),
                            Text = pageNumber.ToString()
                        });
                    }
                }
                #endregion

                viewModel.Pages = new SelectList(pageSelectListItems, "Value", "Text", viewModel.PageNumber);
            }
            ViewData["ExpensesReport"] = viewModel;
            return PartialView("_ExpensesReport", viewModel);
        }

        //public IActionResult Export()
        //{
        //    ExpensesReportAjaxIndexViewModel viewModel = ViewData["ExpensesReport"];

        //}
    }
}

