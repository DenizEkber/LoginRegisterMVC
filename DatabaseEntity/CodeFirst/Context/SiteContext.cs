using DatabaseEntity.CodeFirst.Configuration.UserData;
using DatabaseEntity.CodeFirst.Entity.UserData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity.CodeFirst.Context
{
    public class SiteContext :DbContext
    {
        private string connectionString = "Server=USER\\SQLEXPRESS;Database=SiteDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbSet<Users> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsersConfigurator());
            modelBuilder.ApplyConfiguration(new  UserDetailConfigurator());
        }
    }
}
