using AppCore.DataAccess.Repositories.EntityFramework;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories
{
    public abstract class CollectiveRepositoryBase : RepositoryBase<Collective>
    {
        protected CollectiveRepositoryBase(DbContext db) : base(db)
        {

        }
    }

    public class CollectiveRepository : CollectiveRepositoryBase
    {
        public CollectiveRepository(DbContext db) : base(db)
        {

        }
    }
}
