using Ikea.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ikea.InUseClasses
{
    internal class AdminAllOrders
    {
        private static MyDBContext _database = new MyDBContext();


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

        public static async Task RenderAllOrders()
        {



            Console.Clear();

            var sw = new Stopwatch();
            sw.Start();
            var orders = await _database.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Include(o => o.Customer)
                .ToListAsync();

            sw.Stop();

            RenderOrders(orders);

            if (!orders.Any())
            {
                Console.WriteLine("Det finns inga ordrar.");
                Console.ReadLine();
                AdminMenu.RenderAdminMenu();
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine($"Order {order.Id} | {order.OrderDate:g} | Status: ");

                switch (order.Status)
                {
                    //Testat lägga till färger
                    case OrderStatus.Mottagen:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case OrderStatus.Behandlas:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case OrderStatus.Skickad:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case OrderStatus.Levererad:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }

                Console.WriteLine(order.Status);
                Console.ResetColor();

                Console.WriteLine($"Kund: {order.Customer.Name}");
                Console.WriteLine($"Frakt: {order.ShippingMethod}");
                Console.WriteLine($"Betalning: {order.PaymentMethod}");
                Console.WriteLine($"Totalt: {order.TotalPrice:C2}");
                Console.WriteLine("Innehåll: ");

                foreach (var item in order.Items)
                {
                    Console.WriteLine($"- {item.Product.Name} x {item.Quantity} a {item.Price:C2}");
                }

                Console.WriteLine(new string('-', 50));
            }

            Console.WriteLine();
            Console.WriteLine($"Hela hämtningen tog {sw.ElapsedMilliseconds} ms (millisekunder)");
            Console.WriteLine();
            Console.WriteLine("Ange orderid för att redigera eller 0 för att gå tillbaka");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out var orderId))
            {
                Console.WriteLine("Felaktig inmatning");
                return;
            }
            if (orderId == 0)
            {
                AdminMenu.RenderAdminMenu();
                return;
            }

            var selectedOrder = orders.FirstOrDefault(o => o.Id == orderId);
            if (selectedOrder == null)
            {
                Console.WriteLine("OrderId finns inte"); 
                return;
            }

            Console.Clear();
            Console.WriteLine($"Redigerar order {selectedOrder.Id}");
            Console.WriteLine($"Nuvarande status: {selectedOrder.Status}");
            Console.WriteLine();

            Console.WriteLine("Välj ny status: ");
            foreach (var status in Enum.GetValues(typeof(OrderStatus)))
            {
                Console.WriteLine($"{(int)status}. {status}");
            }

            var statusInput = Console.ReadLine();

            if (!int.TryParse(statusInput, out var statusValue) ||
                !Enum.IsDefined(typeof(OrderStatus), statusValue))
            {
                Console.WriteLine("Felaktig status");
                return;
            }

            selectedOrder.Status = (OrderStatus)statusValue;
            _database.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("SPARAD!");
            Console.ResetColor();

            Console.WriteLine("Klicka enter för att gå tillbaka");

            Console.ReadLine();
            AdminMenu.RenderAdminMenu();
        }
    }
}
