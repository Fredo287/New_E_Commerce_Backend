using E_Commerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionSting = "Server=DESKTOP-OK7AHA9\\SQLEXPRESS; Database=E_Commerce_Backend;Trusted_Connection=True;Encrypt=False; User Id=sa; Password=admin";
            optionsBuilder.UseSqlServer(connectionSting);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
             new ProductCategory
             {
                ProductCategoryId = 1,
                Name = "Electronics",
                Description = "Typical electronic devices."
             },
            new ProductCategory
            {
                ProductCategoryId = 2,
                Name = "Electrical",
                Description = "Typical electrical equipment."
            },
            new ProductCategory
            {
                ProductCategoryId = 3,
                Name = "Clothes",
                Description = "Variety of clothes."
            }
            );
        }
    }
}
