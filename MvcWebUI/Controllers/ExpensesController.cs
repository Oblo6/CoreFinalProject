using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Security.Claims;

namespace MvcWebUI.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly ICollectiveService _collectiveService;
        private readonly ICollectiveUserService _collectiveUserService;

        public ExpensesController(IExpenseService expenseService, ICollectiveService collectiveService, ICollectiveUserService collectiveUserService)
        {
            _expenseService = expenseService;
            _collectiveService = collectiveService;
            _collectiveUserService = collectiveUserService;
        }

        // GET: Expenses
        public IActionResult Index()
        {
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var query = _expenseService.Query().Where(model => model.UserId == Convert.ToInt32(userId));
            var model = query.ToList();
            return View(model);
        }

        // GET: Expenses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("NotFound");
            var expense = _expenseService.Query();
            var model = expense.SingleOrDefault(e => e.Id == id.Value);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        // GET: Expenses/Create
        public IActionResult Create(int? id)
        {
            var model = new ExpenseModel();
            var collectiveId = _collectiveUserService.Query().SingleOrDefault(cu => cu.Id == id).CollectiveId;
            model.CollectiveUserId = id.Value;
            ViewBag.CollectiveId = collectiveId;
            return View(model);
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseModel expense, int id)
        {
            var collectiveId = _collectiveUserService.Query().Where(cu => cu.Id == id).SingleOrDefault().CollectiveId;
            expense.PayDate = DateTime.Now;
            expense.CollectiveUserId = id;
            if (ModelState.IsValid)
            {
                var result = _expenseService.Add(expense);
                if (result.Status == ResultStatus.Success)
                    return RedirectToAction("ExpensesList", "Collectives", new { Id = collectiveId });
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(expense);
                }
                throw new Exception(result.Message);
            }
            return View(expense);
        }


        public IActionResult CreateExp()
        {
            var model = new ExpenseModel();
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            model.UserId = Convert.ToInt32(userId);
            var collectives = _collectiveService.Query().Where(c => c.UserIds.Contains(Convert.ToInt32(userId)));
            ViewBag.Collectives = new SelectList(collectives, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExp(ExpenseModel expense)
        {
            expense.PayDate = DateTime.Now;
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            expense.UserId = Convert.ToInt32(userId);
            var cu = _collectiveUserService.Query().Where(c => c.UserId == expense.UserId && c.CollectiveId == expense.CollectiveId).SingleOrDefault();
            expense.CollectiveUserId = cu.Id;
            if (ModelState.IsValid)
            {
                var result = _expenseService.Add(expense);
                if (result.Status == ResultStatus.Success)
                    return RedirectToAction("ExpensesList", "Collectives", new { Id = expense.CollectiveId });
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(expense);
                }
                throw new Exception(result.Message);
            }
            return View(expense);
        }
        
        // GET: Expenses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View("NotFound");
            var query = _expenseService.Query();
            var model = query.SingleOrDefault(e => e.Id == id.Value);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                var _expense = _expenseService.Query().SingleOrDefault(e => e.Id == expense.Id);
                _expense.Description = expense.Description;
                _expense.Cost = expense.Cost;
                var result = _expenseService.Update(_expense);
                var collectiveId = _expense.CollectiveId;
                if (result.Status == ResultStatus.Success)
                    return RedirectToAction("ExpensesList", "Collectives", new { Id = collectiveId });
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(_expense);
                }
                throw new Exception(result.Message);
            }
            return View(expense);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Login", "Accounts");
            if (!id.HasValue)
                return View("NotFound");
            var collectiveId = _expenseService.Query().SingleOrDefault(e => e.Id == id).CollectiveId;
            var result = _expenseService.Delete(id.Value);
            if (result.Status == ResultStatus.Success)
                return RedirectToAction("ExpensesList", "Collectives", new { Id = collectiveId });
            throw new Exception(result.Message);
        }

        public IActionResult AllDelete(int? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Login", "Accounts");
            var expenses = _expenseService.Query().Where(e => e.CollectiveId == id);
            var expensesIds = expenses.Select(e => e.Id).ToList();
            var result = _expenseService.AllDelete(expensesIds);
            if (result.Status == ResultStatus.Success)
                return RedirectToAction("ExpensesList", "Collectives", new { Id = id });
            throw new Exception(result.Message);
        }
    }
}
