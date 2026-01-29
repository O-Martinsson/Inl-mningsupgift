using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    internal class AdminProductDeletionView
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static Customer _loggedInCustomer = null;
        public static void DeleteProduct()
        {
            Console.Clear();

            var products = _productService.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}. {p.Name}");
            }
            Console.WriteLine("Välj produktId att radera: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Error,Try Again");
                AdminMenu.RenderAdminMenu();
                return;
            }

            var product = products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                Console.WriteLine("Error,Try Again");
                AdminMenu.RenderAdminMenu();
            }

            _productService.DeleteProduct(product);

            Console.WriteLine($"{product} är raderad");
            Console.ReadLine();
            AdminMenu.RenderAdminMenu();
        }
    }
}
