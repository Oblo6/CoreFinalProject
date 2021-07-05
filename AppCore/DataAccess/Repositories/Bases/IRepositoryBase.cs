using System;
using System.Linq;
using AppCore.Records;

namespace AppCore.DataAccess.Repositories.Bases
{
    //public interface IRepository<TEntity>
    //public interface IRepository<TEntity> where TEntity : class
    //public interface IRepository<TEntity> where TEntity : class, new()
    //public interface IRepository<TEntity> where TEntity : RecordBase, new()
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Record, new()
    {
        IQueryable<TEntity> Query();        
        void Add(TEntity entity, bool save = true);
        void Update(TEntity entity, bool save = true);
        void Delete(TEntity entity, bool save = true);
        int Save();
    }
}
