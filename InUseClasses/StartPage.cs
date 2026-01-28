using Ikea.Services;
using Ikea.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Ikea.InUseClasses;


namespace Ikea.InUseClasses
{
    internal class StartPage
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;

        public static void StartPageRender()
        {
            Console.Clear();

            var nameBox = new Window($"Välkommen till *IKEA*", 22, 1, new List<string> { "Dina Möbler Enkelt monterade" });
            nameBox.Draw();

            var products = _productService.GetSpecialOfferProducts();

            for (var i = 0; i < products.Count; i++)
            {
                var left = 2;
                var product = products[i];

                var specialOffer = new List<string>();
                specialOffer.Add(product.Name);
                specialOffer.Add("Kategori: " + product.Category?.Name);
                specialOffer.Add(product.Price.ToString("C2"));
                specialOffer.Add("");

                switch (i)
                {
                    case 0:
                        left = 2;
                        specialOffer.Add("Klicka A för att köpa");
                        break;
                    case 1:
                        left = 30;
                        specialOffer.Add("Klicka B för att köpa");
                        break;
                    case 2:
                        left = 60;
                        specialOffer.Add("Klicka C för att köpa");
                        break;
                }
                var SpecialOfferWindow = new Window($"Erbjudande {i + 1}", left, 6, specialOffer);
                SpecialOfferWindow.Draw();
            }

            Console.WriteLine("\nVälj Erbjudande eller klicka enter för att komma vidare till huvudmeny");

            var userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                /*MAINMENU*/
            }
            //else
            //{
            //    switch (userInput.ToLower())
            //    {
            //        case "a":
            //            var product = products[0];
            //            RenderProduct(product, StartPageRender);
            //            break;
            //        case "b":
            //            var secondProduct = products[1];
            //            RenderProduct(secondProduct, StartPageRender);
            //            break;
            //        case "c":
            //            var thirdProduct = products[2];
            //            RenderProduct(thirdProduct, StartPageRender);
            //            break;
            //        default:
            //            throw new ArgumentException("Felaktig inmatning");
            //            break;
            //    }
            //}

        }


    }
}
