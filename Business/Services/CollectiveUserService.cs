using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using DataAccess.EntityFramework.Repositories;
using System;
using System.Linq;

namespace Business.Services
{
    public interface ICollectiveUserService : IService<CollectiveUserModel>
    {        
    }

    public class CollectiveUserService : ICollectiveUserService
    {
        private readonly CollectiveUserRepositoryBase _collectiveUserRepository;

        public CollectiveUserService(CollectiveUserRepositoryBase collectiveRepositoryBase)
        {
            _collectiveUserRepository = collectiveRepositoryBase;
        }

        public Result Add(CollectiveUserModel model)
        {
            throw new NotImplementedException();
        }        

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _collectiveUserRepository?.Dispose();
        }

        public IQueryable<CollectiveUserModel> Query()
        {
            var query = _collectiveUserRepository.Query().Select(cu => new CollectiveUserModel()
            {
                Id = cu.Id,
                UserId = cu.UserId,
                CollectiveId = cu.CollectiveId
            });
            return query;
        }

        public Result Update(CollectiveUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
