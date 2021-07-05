using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MvcWebUI.Controllers
{
    public class CollectivesController : Controller
    {
        private readonly ICollectiveService _collectiveService;
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;
        private readonly ICollectiveUserService _collectiveUserService;

        public CollectivesController(ICollectiveService collectiveService, IUserService userService, IExpenseService expenseService, ICollectiveUserService collectiveUserService)
        {
            _collectiveService = collectiveService;
            _userService = userService;
            _expenseService = expenseService;
            _collectiveUserService = collectiveUserService;
        }

        // GET: Collectives
        public IActionResult Index()
        {
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var query = _collectiveService.Query().Where(c => c.Users.Any(c => c.Id == Convert.ToInt32(userId)));
            return View(query);
        }

        public IActionResult AllIndex()
        {
            var query = _collectiveService.Query();
            return View(query);
        }

        public IActionResult ExpensesList(int id)
        {
            var query = _expenseService.Query();
            var model = query.Where(e => e.CollectiveId == id).ToList();
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var collective = _collectiveService.Query().SingleOrDefault(c => c.Id == id);
            var cu = _collectiveUserService.Query().Where(c => c.CollectiveId == id);
            if (collective.Users.Any(c => c.Id == Convert.ToInt32(userId)))
            {
                var cuId = cu.Where(c => c.UserId == Convert.ToInt32(userId)).SingleOrDefault().Id;
                ViewBag.Id = cuId;
            }
            else
            {
                ViewBag.Message = "You are not affiliated with the group.";
                return View("_Error");
            }
            var _userNamesHtml = _collectiveService.Query().SingleOrDefault(c => c.Id == id).UserNamesHtml;
            var _collectiveId = _collectiveService.Query().SingleOrDefault(c => c.Id == id).Id;
            var _collectiveName = _collectiveService.Query().SingleOrDefault(c => c.Id == id).Name;
            ViewData["Names"] = _userNamesHtml;
            ViewData["CollectiveName"] = _collectiveName;
            ViewData["CollectiveId"] = _collectiveId;
            return View(model);
        }

        public IActionResult Calculation(int id)
        {
            var collective = _collectiveService.Query().SingleOrDefault(c => c.Id == id);
            return View(collective);
        }

        // GET: Collectives/Create
        public IActionResult Create()
        {
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var model = new CollectiveModel();
            List<UserModel> users = _userService.Query().Where(u => u.Id != Convert.ToInt32(userId)).ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CollectiveModel collective)
        {
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var user = _userService.Query().SingleOrDefault(c => c.Id == Convert.ToInt32(userId));
            collective.CreatedBy = user.UserName;
            collective.CreatedDate = DateTime.Now;
            collective.UserIds.Add(Convert.ToInt32(userId));
            if (ModelState.IsValid)
            {
                var result = _collectiveService.Add(collective);
                if (result.Status == ResultStatus.Success)
                    return RedirectToAction(nameof(Index));
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(collective);
                }
                throw new Exception(result.Message);
            }
            return View(collective);
        }

        public IActionResult AddUser(int? id)
        {
            var collective = _collectiveService.Query().SingleOrDefault(c => c.Id == id);
            var ids = collective.UserIds;
            var allUserIds = _userService.Query().Select(c => c.Id).ToList();
            var users = _userService.Query();
            var idList = new List<int>();
            foreach (var _userId in allUserIds)
                if (!ids.Contains(_userId))
                    idList.Add(_userId);
            var _users = new List<UserModel>();
            foreach (var user in users)
                if (idList.Contains(user.Id))
                    _users.Add(user);
            ViewBag.Users = new SelectList(_users, "Id", "UserName");

            return View(collective);
        }

        [HttpPost]
        public IActionResult AddUser(CollectiveModel model)
        {
            var collective = _collectiveService.Query().SingleOrDefault(c => c.Id == model.Id);
            collective.UserIds = model.UserIds;
            var result = _collectiveService.AddUser(collective);
            if (result.Status == ResultStatus.Success)
                return RedirectToAction("ExpensesList", "Collectives", new { Id = model.Id });
            if (result.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", result.Message);
                return View(collective);
            }
            throw new Exception(result.Message);
        }



        public IActionResult RemoveUser(int? id)
        {
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            List<UserModel> users = _collectiveService.Query().SingleOrDefault(c => c.Id == id).Users.Where(u => u.Id != Convert.ToInt32(userId)).ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            if (id == null)
                return View("NotFound");
            var query = _collectiveService.Query();
            var model = query.SingleOrDefault(c => c.Id == id.Value);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveUser(CollectiveModel collective)
        {
            var query = _collectiveService.Query();
            var model = query.SingleOrDefault(c => c.Id == collective.Id);
            model.UserIds = collective.UserIds;

            var result = _collectiveService.RemoveUser(model);
            if (result.Status == ResultStatus.Success)
                return RedirectToAction("ExpensesList", "Collectives", new { Id = collective.Id });
            if (result.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", result.Message);
                string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                List<UserModel> users = _collectiveService.Query().SingleOrDefault(c => c.Id == collective.Id).Users.Where(u => u.Id != Convert.ToInt32(userId)).ToList();
                ViewBag.Users = new SelectList(users, "Id", "UserName");
                return View(collective);
            }
            throw new Exception(result.Message);
        }

        public IActionResult ExitGroup(int? id)
        {
            var collective = _collectiveService.Query().Where(c => c.Id == id).SingleOrDefault();
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var cu = _collectiveUserService.Query().Where(cu => cu.UserId == Convert.ToInt32(userId));
            var cuId = cu.Where(cu => cu.CollectiveId == id).SingleOrDefault().Id;
            var result = _collectiveService.ExitGroup(cuId, collective, Convert.ToInt32(userId));
            if (result.Status == ResultStatus.Success)
                return RedirectToAction(nameof(Index));
            if (result.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", result.Message);
                ViewBag.Message = result.Message;
                return View("_Error");
            }
            throw new Exception(result.Message);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("NotFound");
            var query = _collectiveService.Query();
            var model = query.SingleOrDefault(c => c.Id == id.Value);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View("NotFound");
            var query = _collectiveService.Query();
            var model = query.SingleOrDefault(c => c.Id == id.Value);
            if (model == null)
                return View("NotFound");

            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var collective = _collectiveService.Query().SingleOrDefault(c => c.Id == id);
            var collectiveInUsers = collective.Users;
            var users = _userService.Query().ToList();

            ViewBag.Users = new MultiSelectList(users, "Id", "UserName", collective.UserIds);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CollectiveModel collective)
        {
            if (ModelState.IsValid)
            {
                var result = _collectiveService.Update(collective);
                if (result.Status == ResultStatus.Success)
                    return RedirectToAction("ExpensesList", "Collectives", new { Id = collective.Id });
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(collective);
                }
                throw new Exception(result.Message);
            }
            return View(collective);
        }

        public IActionResult Delete(int? id)
        {
            var collective = _collectiveService.Query().Where(c => c.Id == id).SingleOrDefault();
            if (!id.HasValue)
                return View("NotFound");
            var result = _collectiveService.CloseGroup(collective, id.Value);
            if (result.Status == ResultStatus.Success)
                return RedirectToAction(nameof(Index));
            if (result.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", result.Message);
                ViewBag.Message = result.Message;
                return View("_Error");
            }
            throw new Exception(result.Message);
        }
    }
}
