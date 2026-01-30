using Ikea.InUseClasses;
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
        public static void RenderProd(List<Product> products, Action goBackAction)
        {
            Console.Clear();
            var boxOptions = new List<string> { "", "0: Gå tillbaka", };

            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                boxOptions.Add($"{i + 1}. {product.Name} - {product.Price:C2}");
            }

            var box = new Window($"Shoppen", 0, 0, boxOptions);
            box.Draw();
            Console.Write("Välj produktnummer för mer info: ");


            var input = Console.ReadLine();

            if (int.TryParse(input, out var choice))
            {
                if (choice == 0)
                {
                    goBackAction();
                    return;
                }
                if (choice > 0 && choice <= products.Count)
                {
                    var selectedProduct = products[choice - 1];
                    RenderProd(selectedProduct, goBackAction);
                }
                else
                {
                    Console.WriteLine("Error: Felinmatning");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error: Felinmatning");
                return;
            }

        }
        private static void RenderProd(Product product, Action goBackAction)
        {
            Console.Clear();
            var boxOptions = new List<string>();
            boxOptions.Add($"Beskrivning: {product.Description}");
            boxOptions.Add($"Kategori: {product.Category?.Name}");
            boxOptions.Add($"Färg: {product.Color}");
            boxOptions.Add($"");
            boxOptions.Add($"0: Gå tillbaka");
            boxOptions.Add($"1: Köp");

            var box = new Window(product.Name, 0, 0, boxOptions);

            box.Draw();

            var userChoice = Console.ReadLine();

            if (int.TryParse(userChoice, out var choice))
            {

                if (choice == 0)
                {
                    goBackAction();
                    return;
                }
                if (choice != 1)
                {
                    //användaren vill inte köpa men tryckt annat än 1 - ladda om produkten bara
                    RenderProd(product, goBackAction);
                    return;
                }
                //användare vill köpa

                Console.WriteLine("Hur många vill du köpa: ");
                var quantityInput = Console.ReadLine();

                if (!int.TryParse(quantityInput, out var quantity))
                {
                    Console.WriteLine("Felaktigt antal, klicka enter för att försöka igen");
                    Console.ReadLine();
                    RenderProd(product, goBackAction);
                    return;
                }
                if (quantity <= 0)
                {
                    Console.WriteLine("Felaktigt antal, klicka enter för att försöka igen");
                    Console.ReadLine();
                    RenderProd(product, goBackAction);
                    return;
                }

                if (CustomerLogInScreen.LoggedInCustomer() == null)
                {
                    Console.WriteLine("Du måste logga in");
                    return;
                }
                if (product.StockQuantity < quantity)
                {
                    Console.WriteLine("Otillräckligt lagersaldo");
                    Console.ReadLine();
                    RenderProd(product, goBackAction);
                    return;
                }
                var cart = _database.Carts
                   .Include(c => c.Items)
                   .FirstOrDefault(c => c.CustomerId == CustomerLogInScreen.LoggedInCustomer().Id);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        CustomerId = CustomerLogInScreen.LoggedInCustomer().Id,
                    };

                    _database.Carts.Add(cart);
                }

                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Quantity = quantity
                });

                _database.SaveChanges();

                Console.WriteLine($"{quantity}st {product.Name} har lagts i kundkorgen, klicka enter för att gå vidare");
                Console.ReadLine();
                goBackAction();
            }
            else
            {
                //användaren har knappat in annat än siffror - gå tillbaka
                goBackAction();
                return;
            }



        }


    }
}
