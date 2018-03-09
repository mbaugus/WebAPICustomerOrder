using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustOrderWebAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base() { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOptional<Customer>(s => s.Customer).WithMany().WillCascadeOnDelete(false);
        }*/
    }
}