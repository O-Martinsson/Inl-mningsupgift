using Ikea.Models;
using Ikea.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    public class CategoryShow
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;

        public static void ListCategorys()
        {
            var categories = _database.Categories.ToList();

            Console.Clear();
            Console.WriteLine("--- VÄLJ KATEGORI ---");

            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }

            Console.WriteLine("0. Gå tillbaka");
            Console.Write("\nVal: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= categories.Count)
            {
                ShowProductsInCategory(categories[index - 1].Id);
            }


        }

        private static void ShowProductsInCategory(int categoryId)
        {
            var products = _database.Products.Where(p => p.CategoryId == categoryId).ToList();
            DisplayProductList(products);
        }
        private static void DisplayProductList(List<Product> products)
        {
            Console.Clear();
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} kr");
            }

            Console.WriteLine("\nVälj en siffra för detaljer/köp, eller 0 för att gå tillbaka.");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= products.Count)
            {
                ProductDetails(products[index - 1]);
            }

        }
        private static void ProductDetails(Product product)
        {
            Console.Clear();
            // Krav: Varje produkt ska kunna väljas för mer info
            Console.WriteLine($"--- {product.Name} ---");
            Console.WriteLine($"Beskrivning: {product.Description}");
            Console.WriteLine($"Pris: {product.Price} kr"); // Krav: Visa pris

            Console.WriteLine("\n1. Köp (Lägg i kundkorg)"); // Krav: Val för köp
            Console.WriteLine("0. Gå tillbaka");

            if (Console.ReadLine() == "1")
            {
                //_cart.Add(product);
                Console.WriteLine($"{product.Name} tillagd!");
                Console.ReadKey();
            }
        }


    }
}
