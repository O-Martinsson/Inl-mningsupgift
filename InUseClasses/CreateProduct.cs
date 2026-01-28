using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    public class CreateProduct
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        public static void AddProduct()
        {
            Console.Clear();

            Console.Write("Namn:");
            var name = Console.ReadLine();

            Console.Write("Pris");
            decimal.TryParse(Console.ReadLine(), out var price);

            Console.Write("Beskrivning");
            var description = Console.ReadLine();

            Console.Write("Lagersaldo:");
            int.TryParse(Console.ReadLine(), out var stock);

            var categories = _categoryService.GetAllCategories();
            foreach (var c  in categories)
            {
                Console.WriteLine($"{c.Id}. {c.Name}");
            }
            Console.Write("Välj kategoriId:");
            if(!int.TryParse(Console.ReadLine(),out var categoryId))
            {
                throw new ArgumentException("Error det måste vara ett existerande id nummer");
                return;
            }
            Console.Write("Erbjudande? (Y)/(N):");
            var isOffer = Console.ReadLine()?.ToLower() == "y";

            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                StockQuantity = stock,
                CategoryId = categoryId,
                IsOnSale = isOffer
            };
            try
            {
                _productService.CreateProduct(product);
                Console.WriteLine("Produkt skapad");
                Console.ReadKey();
                AdminMenu.RenderAdminMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"gick ej att skapa produkten pga: {ex.Message}");
            }

        }

    }
}
