using Ikea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ikea.InUseClasses
{
    internal class Payment
    {
        private static MyDBContext _database = new MyDBContext();
        public static void RenderPayment(string shippingMethod, decimal shippingCost)
        {
            Console.Clear();

            if (CustomerLogInScreen.LoggedInCustomer() == null)
            {
                Console.WriteLine("Du måste logga in för att betala");
                return;
            }

            //Hämta kundkorg för rätt kund
            var cartItems = _database.CartItems
                .Include(c => c.Product)
                .Include(c => c.Cart)
                .Where(c => c.Cart.CustomerId == CustomerLogInScreen.LoggedInCustomer().Id)
                .ToList();

            if (!cartItems.Any())
            {
                Console.WriteLine("Din kundkorg är tom");
                return;
            }

            decimal total = shippingCost;
            foreach (var item in cartItems)
            {
                total += item.Quantity * item.Product.Price;
            }

            Console.WriteLine($"Betalning för din beställning");
            Console.WriteLine($"Frakt: {shippingMethod} ({shippingCost:C2})");
            Console.WriteLine($"Totalt att betala: {total:C2}");
            Console.WriteLine();
            Console.WriteLine("Bekräfta betalning? (J/N)");

            var input = Console.ReadLine();
            if (input?.ToLower() != "j")
            {
                RenderCart.RenderBasket();
                return;
            }

            foreach (var item in cartItems)
            {
                if (item.Product.StockQuantity < item.Quantity)
                {
                   Console.WriteLine ($"Otillräckligt lagersaldo för {item.Product.Name}");
                    return;
                }
            }

            Console.WriteLine("Välj betalsätt");
            Console.WriteLine("1.Kort");
            Console.WriteLine("2.Swish");

            var paymetChoice = Console.ReadLine();
            var paymentMethod = paymetChoice == "2" ? "Swish" : "Kort";

            var order = new Order
            {
                CustomerId = CustomerLogInScreen.LoggedInCustomer().Id,
                OrderDate = DateTime.Now,
                TotalPrice = total,
                ShippingMethod = shippingMethod,
                ShippingCost = shippingCost,
                PaymentMethod = paymentMethod,
                Status = OrderStatus.Mottagen
            };

            _database.Orders.Add(order);

            foreach (var item in cartItems)
            {
                _database.OrderItems.Add(new OrderItem
                {
                    Order = order,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });

                //Dra av lagret
                item.Product.StockQuantity -= item.Quantity;

            }

            _database.CartItems.RemoveRange(cartItems);
            _database.SaveChanges();

            Console.WriteLine("Tack för din beställning! Tryck enter för att gå till huvudmenyn");
            Console.ReadLine();
            MainMenu.MainMenuRender();
        }
    }
}
