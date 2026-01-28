using Ikea.InUseClasses;

using Ikea.Models;
using Microsoft.EntityFrameworkCore;




namespace Ikea
{
    internal class Program
    {
        private static MyDBContext _database = new MyDBContext();
        static void Main(string[] args)
        {
            InUseClasses.StartPage.StartPageRender();
        }
        //Console.WriteLine("--- LÄGG TILL PRODUKT ---");

        //    // 1. Namn och beskrivning
        //    Console.Write("Namn: ");
        //    string name = Console.ReadLine() ?? "";

        //Console.Write("Beskrivning: ");
        //    string desc = Console.ReadLine() ?? "";

        //// 2. Pris
        //Console.Write("Pris: ");
        //    decimal price = decimal.Parse(Console.ReadLine() ?? "0");

        //var newProduct = new Product
        //{
        //    Name = name,
        //    Description = desc,
        //    Price = price,
        //    IsOnSale = false
        //};

        //_database.Products.Add(newProduct);
        //    _database.SaveChanges();

            
    }
}
