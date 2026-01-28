using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    public class CustomerCreation
    {
        private static MyDBContext _database = new MyDBContext();
        private static CustomerService _customerService = new CustomerService(_database);
        public static void RegisterCustomer()
        {
            Console.Clear();

            Console.WriteLine("Registrera ny kund");

            Console.WriteLine("Namn:");
            var name = Console.ReadLine();

            Console.WriteLine("Email: ");
            var email = Console.ReadLine();

            Console.WriteLine("Adress: ");
            var address = Console.ReadLine();

            Console.WriteLine("Ort: ");
            var city = Console.ReadLine();

            Console.WriteLine("Telefonnummer: ");
            var phone = Console.ReadLine();

            Console.WriteLine("Välj lösen: ");
            var userPassword = Console.ReadLine();

            //spara i databasen
            try
            {
                _customerService.CreateCustomer(name, email, address, city, phone, userPassword);
                Console.WriteLine("Kund skapad, klicka för att gå tillbaka");
                Console.ReadLine();
                MainMenu.MainMenuRender();
            }
            catch (Exception e)
            {

                Console.WriteLine($"Det gick inte att skap en kund pga: {e.Message}");
                Console.ReadLine();
                MainMenu.MainMenuRender();
            }
        }
    }
}
