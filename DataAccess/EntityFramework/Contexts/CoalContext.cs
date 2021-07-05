using AppCore.DataAccess.Configs;
using DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Contexts
{
    public class CoalContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Collective> Collectives { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasOne(expense => expense.CollectiveUser)
                .WithMany(cu => cu.Expenses)
                .HasForeignKey(expense => expense.CollectiveUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CollectiveUser>()
                .HasOne(cu => cu.User)
                .WithMany(user => user.CollectiveUsers)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CollectiveUser>()
                .HasOne(cu => cu.Collective)
                .WithMany(collective => collective.CollectiveUsers)
                .HasForeignKey(cu => cu.CollectiveId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(userDetail => userDetail.Country)
                .WithMany(country => country.UserDetails)
                .HasForeignKey(userDetail => userDetail.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(userDetail => userDetail.City)
                .WithMany(city => city.UserDetails)
                .HasForeignKey(userDetail => userDetail.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(user => user.UserDetail)
                .WithOne(userDetail => userDetail.User)
                .HasForeignKey<User>(user => user.UserDetailId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasIndex(userDetail => userDetail.EMail)
                .IsUnique();
        }
    }
}
