using Ikea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;


namespace Ikea.InUseClasses
{
    internal class OrderView
    {

        private static MyDBContext _database = new MyDBContext();

        public static void RenderMyOrders()
        {
            if (CustomerLogInScreen.LoggedInCustomer() == null)
            {
                Console.WriteLine("Du måste logga in");
                return;
            }

            Console.Clear();
            var orders = _database.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.CustomerId == CustomerLogInScreen.LoggedInCustomer().Id)
                .ToList();

            if (!orders.Any())
            {
                Console.WriteLine("Inga ordrar ännu ");
                Console.ReadLine();
                MainMenu.MainMenuRender();
                return;
            }

            RenderOrders(orders);
            Console.WriteLine("Tryck enter för att återgå till huvudmeny");
            Console.ReadLine();
            MainMenu.MainMenuRender();
        }

        private static void RenderOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine($"Order - {order.Id} - {order.OrderDate}");
                Console.WriteLine($"Status: {order.Status}");
                Console.WriteLine($"Frakt: {order.ShippingMethod} ({order.ShippingCost:C})");
                Console.WriteLine($"Betalning: {order.PaymentMethod}");
                Console.WriteLine($"Total: {order.TotalPrice}");
                Console.WriteLine("Innehåll: ");

                foreach (var item in order.Items)
                {
                    Console.WriteLine($" - {item.Product.Name} x {item.Quantity} a {item.Price}");
                }
            }
        }
    }
}
