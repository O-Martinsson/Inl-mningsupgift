using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.Models
{
    public class MyDBContext : DbContext
    {

       // public const string CONNECTION_STRING = "Server=localhost\\SQLEXPRESS01;Database=IKEA;Trusted_Connection=True; TrustServerCertificate=True;";

        public const string CONNECTION_STRING = "Server=tcp:oliverdb.database.windows.net,1433;Initial Catalog=Oliverdb;Persist Security Info=False;User ID=dbadmin;Password=HejHejHej123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=IKEA;Trusted_Connection=True; TrustServerCertificate=True;");

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:oliverdb.database.windows.net,1433;Initial Catalog=Oliverdb;Persist Security Info=False;User ID=dbadmin;Password=HejHejHej123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
