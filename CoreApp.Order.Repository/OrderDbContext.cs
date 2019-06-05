using Microsoft.EntityFrameworkCore;
using System;
using CoreApp.Order.Model;

namespace CoreApp.Order.Repository
{
    public class OrderDbContext : DbContext
    {
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            
        }
         
    }
}
