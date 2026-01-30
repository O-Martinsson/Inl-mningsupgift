using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    public class CustomerLogInScreen
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer;

        //hämtar och leverear vilken kund som är inloggad
        public static Customer LoggedInCustomer()
        { 
            return _loggedInCustomer;
        }

        // loggar ut kund
        public static void LogOutCustomer()
        {
            _loggedInCustomer = null;
        }


        // in parametrar vid inloggning
        public static void CustomerLogIn()
        {
            Console.Clear();

            Console.Write("Kund-login");

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Lösenord: ");
            var password = Console.ReadLine();




            //var customer = _database.Customers.FirstOrDefault();
            
            // kollar inparametar mot databas och kollar så kund finns
            var customer = _customerService.GetByLogIn(email, password);
            if (customer != null)
            {
                Console.WriteLine($"Inloggad som {customer.Name}");

                _loggedInCustomer = customer;
                Console.ReadLine();
                MainMenu.MainMenuRender();
            }
            else
            {
                Console.WriteLine("Fel Inloggning, tryck enter för att försöka igen");
                Console.ReadLine();
                CustomerLogIn();
                return;

            }

                
            
           
        }



    }
}
