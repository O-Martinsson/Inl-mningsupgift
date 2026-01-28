using Ikea.Services;
using Ikea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikea.InUseClasses;

namespace Ikea.InUseClasses
{
    internal class RenderProducts
    {

        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;


        public static void RenderAllProducts()
        {
            Console.Clear();

            var products = _productService.GetAllProducts();
            RenderProd(products, MainMenu.MainMenuRender);
        }
        private static void RenderProd(List<Product> products, Action goBackAction)
        {
            Console.Clear();
            var boxOption = new List<string> {"","0: Gå tillbaka" };

            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                boxOption.Add($"{i + 1}.{product.Name} - {product.Price:C2}");
            }

        }


    }
}
