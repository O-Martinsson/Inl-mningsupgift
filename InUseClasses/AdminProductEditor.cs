using Ikea.InUseClasses;
using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    internal class AdminProductEditor
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        public static void RenderEditProduct()
        {
            var products = _productService.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}. {p.Name}");
            }

            Console.Write("Välj produktId: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Error,Try again");
                AdminMenu.RenderAdminMenu();
                return;
            }

            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Error,Try again");
                AdminMenu.RenderAdminMenu();
            }

            //Namn
            Console.WriteLine($"Nytt namn ({product.Name}) ");
            var name = Console.ReadLine();

            Console.WriteLine($"Ny beskrivning ({product.Description}):  ");
            var description = Console.ReadLine();

            // Pris
            Console.WriteLine($"Nytt pris ({product.Price})  ");
            var priceInput = Console.ReadLine();

            // Specialerbjudande
            Console.WriteLine($"Specialerbjudande ({(product.IsOnSale ? "j" : "n")}) ");
            var offerInput = Console.ReadLine();

            try
            {
                _productService.UpdateProduct(product.Id, name, description, priceInput,offerInput);
                Console.WriteLine("Produkt uppdaterad ");
                Console.ReadLine();
                AdminMenu.RenderAdminMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error msg{e.Message}");
            }
        }
    }
}
