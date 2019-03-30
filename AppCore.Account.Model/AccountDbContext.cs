using CoreApp.Account.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreApp.Account.Model
{
    public class AccountDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
            Users.AddRange(new User
            {
                ID = 1,
                Name = "Raj"
            },
                new User
                {
                    ID = 2,
                    Name = "Alok"
                });

            this.SaveChanges();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasData(
            //     new User
            //     {
            //         ID = 1,
            //         Name = "Raj"
            //     },
            //    new User
            //    {
            //        ID = 2,
            //        Name = "Alok"
            //    }
            //);
            //this.SaveChanges();
        }
    }
}
