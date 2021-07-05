using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using DataAccess.EntityFramework.Entities;
using DataAccess.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ICollectiveService : IService<CollectiveModel>
    {
        Task<Result<List<CollectiveModel>>> GetCollectivesAsync();
        Result RemoveUser(CollectiveModel model);
        Result AddUser(CollectiveModel model);
        Result ExitGroup(int id, CollectiveModel model, int userId);
        Result CloseGroup(CollectiveModel model, int id);
    }

    public class CollectiveService : ICollectiveService
    {
        private readonly CollectiveRepositoryBase _collectiveRepository;
        private readonly CollectiveUserRepositoryBase _collectiveUserRepository;
        private readonly ExpenseRepositoryBase _expenseRepository;

        public CollectiveService(CollectiveRepositoryBase collectiveRepository, CollectiveUserRepositoryBase collectiveUserRepository, ExpenseRepositoryBase expenseRepository)
        {
            _collectiveRepository = collectiveRepository;
            _collectiveUserRepository = collectiveUserRepository;
            _expenseRepository = expenseRepository;
        }

        public Result Add(CollectiveModel model)
        {
            try
            {
                var entity = new Collective()
                {
                    Name = model.Name,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    CollectiveUsers = (model.UserIds ?? new List<int>()).Select(uId => new CollectiveUser()
                    {
                        UserId = uId
                    }).ToList()
                };
                _collectiveRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result CloseGroup(CollectiveModel model, int id)
        {
            try
            {
                var collective = _collectiveRepository.EntityQuery(c => c.Id == id).SingleOrDefault();
                var collectiveUsers = _collectiveUserRepository.EntityQuery(cu => cu.CollectiveId == id);
                if (model.Users.Any(c => c.Expenses.Count > 0))
                    return new ErrorResult("Collective has expenses so it can't be deleted!");
                if (model.Users != null && model.Users.Count > 0)
                {
                    foreach (var item in collectiveUsers)
                    {
                        _collectiveUserRepository.Delete(item, false);
                    }
                }
                _collectiveRepository.Delete(collective);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }


        public void Dispose()
        {
            _collectiveRepository?.Dispose();
        }

        public IQueryable<CollectiveModel> Query()
        {
            var query = _collectiveRepository.Query().Select(c => new CollectiveModel()
            {
                Id = c.Id,
                Guid = c.Guid,
                Name = c.Name,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                UserCount = c.CollectiveUsers.Count,
                UserIds = c.CollectiveUsers.Select(cu => cu.UserId).ToList(),
                Users = c.CollectiveUsers.Select(u => new UserModel()
                {
                    Id = u.User.Id,
                    UserName = u.User.UserName,
                    Expenses = u.Expenses.Select(e => new ExpenseModel()
                    {
                        Id = e.Id,
                        Guid = e.Guid,
                        CollectiveId = e.CollectiveUser.Collective.Id,
                        Description = e.Description,
                        Cost = e.Cost,
                        PayDate = e.PayDate,
                        CollectiveUserId = e.CollectiveUserId,
                        UserId = e.CollectiveUser.User.Id,
                        UserName = e.CollectiveUser.User.UserName,
                        CollectiveName = e.CollectiveUser.Collective.Name
                    }).ToList()
                }).ToList()
            });
            return query;
        }

        public Result Update(CollectiveModel model)
        {
            try
            {
                var entity = _collectiveRepository.EntityQuery(c => c.Id == model.Id).SingleOrDefault();
                entity.Name = model.Name.Trim();
                entity.CollectiveUsers = (model.UserIds ?? new List<int>()).Select(uId => new CollectiveUser()
                {
                    UserId = uId
                }).ToList();
                _collectiveRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result AddUser(CollectiveModel model)
        {
            try
            {
                foreach (var userId in model.UserIds)
                {
                    var entity = new CollectiveUser()
                    {
                        CollectiveId = model.Id,
                        UserId = userId
                    };
                    _collectiveUserRepository.Update(entity);
                }
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result RemoveUser(CollectiveModel model)
        {
            try
            {
                var entity = _collectiveUserRepository.Query().Where(e => e.CollectiveId == model.Id);
                foreach (var userId in model.UserIds)
                {
                    var deleteEntity = entity.SingleOrDefault(e => e.UserId == userId);
                    if (model.Users.SingleOrDefault(u => u.Id == userId).Expenses != null && model.Users.SingleOrDefault(u => u.Id == userId).Expenses.Count > 0)
                        return new ErrorResult("the user has expenses in the group so the user can't be removed");
                    _collectiveUserRepository.Delete(deleteEntity);
                }
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result ExitGroup(int id, CollectiveModel model, int userId)
        {
            try
            {
                var cu = _collectiveUserRepository.EntityQuery(cu => cu.Id == id).SingleOrDefault();
                if (model.Users.SingleOrDefault(u => u.Id == userId).Expenses != null && model.Users.SingleOrDefault(u => u.Id == userId).Expenses.Count > 0)
                    return new ErrorResult("You have expenses in the group so you can't be removed");
                _collectiveUserRepository.Delete(cu);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }


        public async Task<Result<List<CollectiveModel>>> GetCollectivesAsync()
        {
            try
            {
                List<Collective> collectiveEntities = await _collectiveRepository.Query().OrderBy(c => c.Name).ToListAsync();
                List<CollectiveModel> collectives = collectiveEntities.Select(c => new CollectiveModel() { Id = c.Id, Name = c.Name }).ToList();
                return new SuccessResult<List<CollectiveModel>>(collectives);
            }
            catch (Exception exc)
            {
                return new ExceptionResult<List<CollectiveModel>>(exc);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var collective = _collectiveRepository.EntityQuery(c => c.Id == id).SingleOrDefault();
                if (collective.CollectiveUsers.Count > 0)
                    return new ErrorResult("Collective has expenses so it can't be deleted!");
                _collectiveRepository.Delete(collective);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
    }
}
