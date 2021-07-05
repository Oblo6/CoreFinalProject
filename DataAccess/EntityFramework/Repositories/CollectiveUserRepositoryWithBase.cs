using AppCore.DataAccess.Repositories.EntityFramework;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories
{
    public abstract class CollectiveUserRepositoryBase : RepositoryBase<CollectiveUser>
    {
        protected CollectiveUserRepositoryBase(DbContext db) : base(db)
        {

        }
    }

    public class CollectiveUserRepository : CollectiveUserRepositoryBase
    {
        public CollectiveUserRepository(DbContext db) : base(db)
        {

        }
    }
}
