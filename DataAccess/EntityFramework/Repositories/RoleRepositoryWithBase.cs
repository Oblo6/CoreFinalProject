using AppCore.DataAccess.Repositories.EntityFramework;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories
{
    public abstract class RoleRepositoryBase : RepositoryBase<Role>
    {
        protected RoleRepositoryBase(DbContext db) : base(db)
        {

        }
    }

    public class RoleRepository : RoleRepositoryBase
    {
        public RoleRepository(DbContext db) : base(db)
        {

        }
    }
}
