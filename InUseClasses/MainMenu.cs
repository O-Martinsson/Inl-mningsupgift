using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Ikea.InUseClasses;
using Ikea.Models;

namespace Ikea.InUseClasses
{
    public class MainMenu
    {
        public static void MainMenuRender()
        {
            while (true)
            {
                Console.Clear();

                var optionsMenu = new List<string>
            {
                "(1) Startsidan",
                "(2) Se alla produkter",
                "(3) Se alla kategorier",
                "(4) Kundkorg",
                "(5) Sök",
                "(6) Logga in",
                "(7) Mina Ordrar",
                "(8) Logga ut",
                "(9) Admin",
                "(10) Bli kund"
            };
                var mainMenu = new Window("Huvudmeny", 2, 2, optionsMenu);
                mainMenu.Draw();

                var choice = Console.ReadLine();

                if (int.TryParse(choice, out int value))
                {
                    switch (value)
                    {
                        case 1:
                            StartPage.StartPageRender();
                            break;
                        case 2:
                            RenderProducts.RenderAllProducts();
                            break;
                        case 3:
                            CategoryShow.ListCategorys();
                            break;
                        case 4:
                            RenderCart.RenderBasket();
                            break;
                        case 5:
                            SearchFeature.RenderSearch();
                            break;
                        case 6:
                            CustomerLogInScreen.CustomerLogIn();
                            break;
                        case 7:
                            OrderView.RenderMyOrders();
                            break;

                        case 8:
                            CustomerLogInScreen.LogOutCustomer();
                            Console.WriteLine("Du har loggat ut!");
                            Console.ReadKey();
                            StartPage.StartPageRender();

                            break;
                        case 9:
                            RenderAdminLog.RenderAdminLogIn();
                            break;
                        case 10:
                            CustomerCreation.RegisterCustomer();
                            break;
                        default:
                            Console.WriteLine("Ops something broke");
                            Console.ReadKey();
                            StartPage.StartPageRender();

                            break;
                    }

                }
                else
                { 
                    throw new ArgumentException("ERROR U CRASHED MY PROGRAM");
                    Console.ReadKey();
                    MainMenuRender();
                }


            }


        }
    }
}
