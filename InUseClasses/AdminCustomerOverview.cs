using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{

    internal class AdminCustomerOverview
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;
        public static void AdminCustomers()
        {
            Console.Clear();
            var customers = _customerService.GetAllCustomers();

          

            Console.WriteLine("\nVälj kundId för att redigera eller 0 för att gå tillbaka: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0)
            {
                AdminMenu.RenderAdminMenu();
                return;
            }
            foreach (var c in customers)
            {
                Console.WriteLine($"{c.Id}. {c.Name} | {c.Email} | {c.Address} | {c.City} | {c.Phonenumber}");
            }
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                Console.WriteLine("Error, Try again");
                AdminMenu.RenderAdminMenu();
                return;
            }

            Console.WriteLine($"Namn ({customer.Name})");
            var input = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(input)) customer.Name = input;

            Console.WriteLine($"Email ({customer.Email})");
            input = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(input)) customer.Email = input;

            Console.WriteLine($"Address ({customer.Address})");
            input = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(input)) customer.Address = input;

            Console.WriteLine($"Stad ({customer.City})");
            input = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(input)) customer.City = input;

            Console.WriteLine($"Telefonnummer ({customer.Phonenumber})");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                customer.Phonenumber = input;

            _database.SaveChanges();
            Console.WriteLine("Kunduppgifter uppdaterade!");
            Console.ReadLine();
            AdminMenu.RenderAdminMenu();

        }
    }
}
