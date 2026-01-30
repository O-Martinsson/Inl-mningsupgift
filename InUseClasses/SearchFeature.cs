using Ikea.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ikea.InUseClasses
{
    internal class SearchFeature
    {
        public static void RenderSearch()
        {
            Console.Clear();
            var menuOptions = new List<string>();
            menuOptions.Add("0. Tillbaka till huvudmenyn");
            menuOptions.Add("Valfritt sökord");

            var optionBox = new Window("Sök efter produkt", 0, 0, menuOptions);
            optionBox.Draw();

            var input = Console.ReadLine();

            if (input == "0")
            {
                MainMenu.MainMenuRender();
                return;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Du måste ange ett sökord");
                return;
            }

            var query = """
                    SELECT *
                    FROM Products
                    WHERE Name LIKE @search OR Description LIKE @search
                    """;

            try
            {
                using (var conn = new SqlConnection(MyDBContext.CONNECTION_STRING))
                {
                    var products = conn.Query<Product>(
                        query,
                        new { search = $"%{input}%" }
                        ).ToList();

                    Console.Clear();

                    if (!products.Any())
                    {
                        Console.WriteLine("Inga produkter hittades.");
                    }
                    else
                    {
                        foreach (var product in products)
                        {
                            Console.WriteLine($"{product.Name} - {product.Description}");
                        }
                    }
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta..");
                    Console.ReadKey();
                    RenderSearch();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error msg:{ex.Message}");
            }
        }
    }
}
