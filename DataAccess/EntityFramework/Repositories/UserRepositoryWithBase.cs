using AppCore.DataAccess.Repositories.EntityFramework;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories
{
    public abstract class UserRepositoryBase : RepositoryBase<User>
    {
        protected UserRepositoryBase(DbContext db) : base(db)
        {

        }
    }

    public class UserRepository : UserRepositoryBase
    {
        public UserRepository(DbContext db) : base(db)
        {

        }
    }
}
