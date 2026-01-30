using Ikea.InUseClasses;
using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{

    internal class AdminMenu
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;
        private static Admin _loggedInAdmin;
        public static void SetLoggedinAdmin(Admin loggedInAdmin)
        {
            _loggedInAdmin = loggedInAdmin;
        }
        public static void RenderAdminMenu()
        {
            if(_loggedInAdmin == null)
            {
                Console.WriteLine("Du måste vara inloggad som admin");
                Console.ReadKey();
                RenderAdminLog.RenderAdminLogIn();
                return;
            }
            Console.Clear();

            //Skapar och ritar upp Menyn

            Console.WriteLine("Adminmeny:");

            var adminChoices = new List<string>();

            adminChoices.Add("(1)\tSe alla produkter");
            adminChoices.Add("(2)\tSkapa produkt");
            adminChoices.Add("(3)\tSkapa kategori");
            adminChoices.Add("(4)\tRedigera produkt");
            adminChoices.Add("(5)\tRedigera kategori");
            adminChoices.Add("(6)\tTa bort produkt");
            adminChoices.Add("(7)\tSe konton och redigera");
            adminChoices.Add("(8)\tSe lagersaldo");
            adminChoices.Add("(9)\tse alla ordrar");
            adminChoices.Add("(10)\tTillbaka");

            var adminBox = new Window("Admin", 0, 0, adminChoices);
            adminBox.Draw();

            var input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    RenderProducts.RenderAllProducts();
                    break;
                case "2":
                    CreateProduct.AddProduct();
                    break;
                case "3":
                    CreateCategory.RenderCreateCategory();
                    break;
                case "4":
                    AdminProductEditor.RenderEditProduct();
                    break;
                case "5":
                    AdminCategoryEditor.RenderEditCategory();
                    break;
                case "6":
                    AdminProductDeletionView.DeleteProduct();
                    break;
                case "7":
                    AdminCustomerOverview.AdminCustomers();
                    break;
                case "8":
                    AdminStockOverview.RenderAdminInStock();
                    break;
                case "9":
                    AdminAllOrders.RenderAllOrders();
                    break;
                case "10":
                    MainMenu.MainMenuRender();
                    break;


            }


        }
    }
}
