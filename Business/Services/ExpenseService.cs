using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Business.Models.Filters;
using Business.Models.Reports;
using DataAccess.EntityFramework.Entities;
using DataAccess.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Business.Services
{
    public interface IExpenseService : IService<ExpenseModel>
    {
        Result<List<ExpensesReportModel>> GetExpensesReport(ExpenseReportFilterModel filter, PageModel page = null, OrderModel order = null);
        Result AllDelete(List<int> ids);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly CollectiveRepositoryBase _collectiveRepository;
        private readonly CollectiveUserRepositoryBase _collectiveUserRepository;
        private readonly ExpenseRepositoryBase _expenseRepository;
        private readonly UserRepositoryBase _userRepository;

        public ExpenseService(ExpenseRepositoryBase expenseRepository, CollectiveRepositoryBase collectiveRepository, CollectiveUserRepositoryBase collectiveUserRepository, UserRepositoryBase userRepository)
        {
            _expenseRepository = expenseRepository;
            _collectiveRepository = collectiveRepository;
            _collectiveUserRepository = collectiveUserRepository;
            _userRepository = userRepository;
        }

        public Result Add(ExpenseModel model)
        {
            try
            {
                var entity = new Expense()
                {
                    Description = model.Description,
                    Cost = model.Cost,
                    PayDate = model.PayDate,
                    CollectiveUserId = model.CollectiveUserId
                };
                _expenseRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var expense = _expenseRepository.Query().SingleOrDefault(e => e.Id == id);
                _expenseRepository.Delete(expense);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result AllDelete(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                    _expenseRepository.Delete(id);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }


        public void Dispose()
        {
            _expenseRepository?.Dispose();
        }

        public IQueryable<ExpenseModel> Query()
        {
            var query = _expenseRepository.Query().OrderByDescending(e => e.PayDate).Select(e => new ExpenseModel()
            {
                Id = e.Id,
                Guid = e.Guid,
                CollectiveId = e.CollectiveUser.Collective.Id,
                CollectiveName = e.CollectiveUser.Collective.Name,
                Description = e.Description,
                Cost = e.Cost,
                PayDate = e.PayDate,
                CollectiveUserId = e.CollectiveUserId,
                UserId = e.CollectiveUser.User.Id,
                UserName = e.CollectiveUser.User.UserName,
            });
            return query;
        }

        public Result Update(ExpenseModel model)
        {
            var entity = _expenseRepository.EntityQuery(e => e.Id == model.Id).SingleOrDefault();
            entity.CollectiveUserId = model.CollectiveUserId;
            entity.Description = model.Description;
            entity.Cost = model.Cost;
            entity.PayDate = model.PayDate;
            _expenseRepository.Update(entity);
            return new SuccessResult();
        }

        public Result<List<ExpensesReportModel>> GetExpensesReport(ExpenseReportFilterModel filter, PageModel page = null, OrderModel order = null)
        {
            try
            {
                #region Query
                var expenseQuery = _expenseRepository.Query();
                var collectiveUserQuery = _collectiveUserRepository.Query();
                var query = expenseQuery.Join(collectiveUserQuery,
                    e => e.CollectiveUserId,
                    cu => cu.Id,
                    (e, cu) => new ExpensesReportModel()
                    {
                        CollectiveName = cu.Collective.Name,
                        ExpenseDescription = e.Description,
                        Cost = e.Cost,
                        CostText = e.Cost.ToString(),
                        PayDateText = e.PayDate.ToString("MM/dd/yyyy", new CultureInfo("en")),
                        UserName = e.CollectiveUser.User.UserName,
                        CollectiveId = cu.Collective.Id,
                        PayDate = e.PayDate,
                        UserId = cu.User.Id
                    });
                #endregion

                #region Query First Order
                query = query.OrderBy(q => q.CollectiveName).ThenBy(q => q.PayDate); // *1
                #endregion

                #region Order
                if (order != null && !string.IsNullOrWhiteSpace(order.Expression))
                {
                    switch (order.Expression)
                    {
                        case "Collective Name":
                            query = order.DirectionAscending
                                ? query.OrderBy(q => q.CollectiveName)
                                : query.OrderByDescending(q => q.CollectiveName);
                            break;
                        case "Expenses Description":
                            query = order.DirectionAscending
                                ? query.OrderBy(q => q.ExpenseDescription)
                                : query.OrderByDescending(q => q.ExpenseDescription);
                            break;
                        case "Cost":
                            query = order.DirectionAscending
                                ? query.OrderBy(q => q.Cost)
                                : query.OrderByDescending(q => q.Cost);
                            break;
                        case "User Name":
                            query = order.DirectionAscending
                                ? query.OrderBy(q => q.UserName)
                                : query.OrderByDescending(q => q.UserName);
                            break;
                        default:
                            query = order.DirectionAscending
                                ? query.OrderBy(q => q.PayDate)
                                : query.OrderByDescending(q => q.PayDate);
                            break;

                    }
                }
                #endregion

                #region Query Filter
                if (filter.CollectiveId.HasValue)
                    query = query.Where(q => q.CollectiveId == filter.CollectiveId.Value);
                if (!string.IsNullOrWhiteSpace(filter.ExpenseDescription))
                    query = query.Where(q => q.ExpenseDescription.ToUpper().Contains(filter.ExpenseDescription.ToUpper().Trim()));
                if (!string.IsNullOrWhiteSpace(filter.UserName))
                    query = query.Where(q => q.UserName.ToUpper().Contains(filter.UserName.ToUpper().Trim()));
                if (!string.IsNullOrWhiteSpace(filter.CostBeginText))
                {
                    double costBegin = Convert.ToDouble(filter.CostBeginText.Replace(",", "."),
                        CultureInfo.InvariantCulture);
                    query = query.Where(q => q.Cost >= costBegin);
                }
                if (!string.IsNullOrWhiteSpace(filter.CostEndText))
                {
                    double costEnd = Convert.ToDouble(filter.CostEndText.Replace(",", "."),
                        CultureInfo.InvariantCulture);
                    query = query.Where(q => q.Cost <= costEnd);
                }
                if (!string.IsNullOrWhiteSpace(filter.PayDateBeginText))
                {
                    DateTime payDateBegin = DateTime.Parse(filter.PayDateBeginText, new CultureInfo("en"));
                    query = query.Where(q => q.PayDate >= payDateBegin);
                }
                if (!string.IsNullOrWhiteSpace(filter.PayDateEndText))
                {
                    DateTime payDateEnd = DateTime.Parse(filter.PayDateEndText, new CultureInfo("en"));
                    query = query.Where(q => q.PayDate <= payDateEnd);
                }
                if (filter.UserId.HasValue)
                    query = query.Where(q => q.UserId == filter.UserId.Value);
                #endregion

                #region Query Paging
                if (page != null)
                {
                    page.RecordsCount = query.Count();
                    int skip = (page.PageNumber - 1) * page.RecordsPerPageCount;
                    int take = page.RecordsPerPageCount;
                    query = query.Skip(skip).Take(take);
                }
                #endregion

                return new SuccessResult<List<ExpensesReportModel>>(query.ToList());
            }
            catch (Exception exc)
            {
                return new ExceptionResult<List<ExpensesReportModel>>(exc);
            }
        }
    }
}
