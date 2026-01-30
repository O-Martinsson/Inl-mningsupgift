using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ikea.InUseClasses
{
    internal class RenderCart
    {
        private static MyDBContext _database = new MyDBContext();
        


        public static void RenderBasket()
        {

            while (true)
            {
                Console.Clear();
                var boxOptions = new List<string>();

                if (CustomerLogInScreen.LoggedInCustomer() == null)
                {
                    Console.WriteLine("Du måste logga in");
                    Console.ReadKey();
                    return;
                }

                var items = _database.CartItems
                    .Include(p => p.Product)
                    .Include(c => c.Cart)
                    .Where(c => c.Cart.CustomerId == CustomerLogInScreen.LoggedInCustomer().Id)
                    .ToList();

                if (!items.Any())
                {
                    boxOptions.Add("Din kundkorg är tom ");
                }
                else
                {
                    decimal total = 0;
                    for (int i = 0; i < items.Count; i++)
                    {
                        var cartItem = items[i];
                        var itemTotal = cartItem.Quantity * cartItem.Product.Price;
                        boxOptions.Add($"{cartItem.ProductId}.{cartItem.Product.Name} -{cartItem.Quantity} st á {cartItem.Product.Price:C2}, {itemTotal:C2}");
                        total += itemTotal;
                    }
                    boxOptions.Add("");
                    boxOptions.Add($"Totalt: {total:C2}");
                }
                var box = new Window("Kundkorg", 12, 12, boxOptions);
                box.Draw();

                var menuOptions = new List<string>();
                menuOptions.Add("0. Tillbaka till huvudmenyn");
                menuOptions.Add("A. Töm hela kundkorgen");
                menuOptions.Add("B. Gå till betalning");
                menuOptions.Add("(ProduktId).Radera enskild produkt");

                var optionBox = new Window("Dina alternativ", 0, items.Count * 5, menuOptions);
                optionBox.Draw();

                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "0":
                        MainMenu.MainMenuRender();
                        return;
                    case "a":
                        _database.RemoveRange(items);
                        _database.SaveChanges();
                        Console.WriteLine("Kundkorgen är tömd");
                        Console.ReadLine();
                        RenderBasket();
                        break;
                    case "b":
                        if (!items.Any())
                        {
                            Console.WriteLine("Kundkorgen är tom");
                            return;
                        }
                        ShippingLoad.RenderShipping();
                        return;
                    default:

                        if (int.TryParse(input, out var productId))
                        {
                            var item = items.Where(x => x.ProductId == productId).FirstOrDefault();
                            if (item != null)
                            {
                                _database.Remove(item);
                                _database.SaveChanges();
                                Console.WriteLine($"{item.Product.Name} är raderad"); // Fixa så man kan välja produkt att ta bort inte bara tömma hela kundkorgen
                                Console.ReadLine();
                                RenderBasket();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error, try again");
                            RenderCart.RenderBasket();
                        }
                        return;
                }
            }

        }
    }
}
