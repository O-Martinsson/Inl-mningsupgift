using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    public class RenderAdminLog
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;

        private static Admin _loggedInAdmin = null;
        public static void RenderAdminLogIn()
        {
            Console.Clear();
            Console.WriteLine("Admin-Login");
            
            
            // login som admin
            Console.Write("Användarnamn:");
            var adminUser = Console.ReadLine();

            Console.Write("Lösenord:");
            var adminPassword = Console.ReadLine();

            //---------------------------------------


            var hashedPassword = SecurityService.HashPassword(adminPassword);
            Admin admin = _database.Admins
                .FirstOrDefault(a => a.UserName == adminUser && a.PassWord == hashedPassword);

            if (admin == null)
            {
                Console.WriteLine("Fel inmatning, Försök igen");
                Console.ReadKey();
                RenderAdminLogIn();
                return;
            }
            _loggedInAdmin = admin;


        }
        
    }
}
