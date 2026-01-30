using Ikea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ikea.InUseClasses
{
    internal class ShippingLoad
    {
        private static MyDBContext _database = new MyDBContext();
        public static void RenderShipping()
        {
            Console.Clear();

            if (CustomerLogInScreen.LoggedInCustomer() == null)
            {
                Console.WriteLine("Du måste logga in för att beställa");
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

            Console.Clear();

            //Visar kundinformation
            Console.WriteLine($"Namn: {CustomerLogInScreen.LoggedInCustomer().Name}");
            Console.WriteLine($"Telefonnummer: {CustomerLogInScreen.LoggedInCustomer().Phonenumber}");
            Console.WriteLine($"Adress: {CustomerLogInScreen.LoggedInCustomer().Address}");
            Console.WriteLine($"Ort: {CustomerLogInScreen.LoggedInCustomer().City}");
            Console.WriteLine();

            //Frakt alternativ
            Console.WriteLine("1. Postnord (49 Kr)");
            Console.WriteLine("2. DHL (99 Kr)");

            var choice = Console.ReadLine();

            string method = choice == "2" ? "DHL" : "Postnord";
            decimal cost = choice == "2" ? 99 : 49;

            Payment.RenderPayment(method, cost);

        }
    }
}
