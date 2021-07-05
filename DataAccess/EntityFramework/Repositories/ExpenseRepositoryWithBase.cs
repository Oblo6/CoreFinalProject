using AppCore.DataAccess.Repositories.EntityFramework;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories
{
    public abstract class ExpenseRepositoryBase : RepositoryBase<Expense>
    {
        protected ExpenseRepositoryBase(DbContext db) : base(db)
        {

        }
    }

    public class ExpenseRepository : ExpenseRepositoryBase
    {
        public ExpenseRepository(DbContext db) : base(db)
        {

        }
    }
}
