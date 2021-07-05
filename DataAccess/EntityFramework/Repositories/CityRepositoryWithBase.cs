using AppCore.DataAccess.Repositories.EntityFramework;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories
{
    public abstract class CityRepositoryBase : RepositoryBase<City>
    {
        protected CityRepositoryBase(DbContext db) : base(db)
        {

        }
    }

    public class CityRepository : CityRepositoryBase
    {
        public CityRepository(DbContext db) : base(db)
        {

        }
    }
}
