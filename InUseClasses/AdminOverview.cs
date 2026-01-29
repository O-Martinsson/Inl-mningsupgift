using Ikea.Models;
using Dapper;
using Ikea.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    internal class AdminOverview
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);
        private static CustomerService _customerService = new CustomerService(_database);
        private static Customer _loggedInCustomer = null;

        private static void RenderAdminStatistics()
        {
            Console.Clear();

            Console.WriteLine("Statistik för LiloShop");
            Console.WriteLine(new string('-', 50));

            using (var conn = new SqlConnection(MyDBContext.CONNECTION_STRING))
            {
                //Bästsäljande produkt

                var bestSellingQ = """
                    SELECT TOP 3
                        p.Name AS ProductName,
                        SUM(oi.Quantity) AS TotalSold
                    FROM OrderItems oi
                    JOIN Products p ON oi.ProductId = p.Id
                    GROUP BY p.Name
                    ORDER BY TotalSold DESC
                    """;

                Console.WriteLine("\nBästsäljande produkter: ");
                var bestSelling = conn.Query(bestSellingQ).ToList();

                if (!bestSelling.Any())
                    Console.WriteLine("Ingen försäljning finns");
                else
                    foreach (var p in bestSelling)
                        Console.WriteLine($"{p.ProductName} - {p.TotalSold} st");

                //Populäraste kategorin

                var popularCategoryQ = """
                SELECT TOP 1
                    c.Name AS CategoryName,
                    SUM(oi.Quantity) AS TotalSold
                FROM OrderItems oi
                JOIN Products p ON oi.ProductId = p.Id
                JOIN Categories c ON p.CategoryId = c.Id
                GROUP BY c.Name
                ORDER BY TotalSold DESC
                """;

                Console.WriteLine("\nPopuläraste kategorin: ");
                var popularCategory = conn.QueryFirstOrDefault(popularCategoryQ);

                if (popularCategory == null)
                    Console.WriteLine("Ingen kategori-data finns");
                else
                    Console.WriteLine($"{popularCategory.CategoryName} ({popularCategory.TotalSold} sålda produkter)");


                //Lågt lagersaldo

                var lowStockQ = """
                    SELECT Name, StockQuantity
                    FROM Products
                    WHERE StockQuantity < 10
                    ORDER BY StockQuantity
                    """;

                Console.WriteLine("\nProdukter med lågt lagersaldo: ");
                var lowStock = conn.Query(lowStockQ).ToList();

                if (!lowStock.Any())
                    Console.WriteLine("Alla produkter har tillräckligt lagersaldo");
                else
                    foreach (var p in lowStock)
                        Console.WriteLine($"{p.Name} - {p.StockQuantity} kvar");
            }

        }
    }
}
