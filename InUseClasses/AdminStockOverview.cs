using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    internal class AdminStockOverview
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        public static void RenderAdminInStock()
        {
            Console.Clear();
            var products = _productService.GetAllProducts();

            foreach (var p in products)
            {
                Console.WriteLine($"Id: {p.Id} | {p.Name} | Lagersaldo: {p.StockQuantity}");
            }

            Console.WriteLine("\nTryck enter för att återgå till adminmenyn");
            Console.ReadLine();
            AdminMenu.RenderAdminMenu();
        }
    }
}
