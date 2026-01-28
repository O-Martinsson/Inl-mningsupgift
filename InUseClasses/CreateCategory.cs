using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{

    public class CreateCategory
    {
        private static MyDBContext _database = new MyDBContext();
        private static ProductService _productService = new ProductService(_database);
        private static CategoryService _categoryService = new CategoryService(_database);


        public static void RenderCreateCategory()
        {
            Console.Clear();

            Console.Write("Ange Kategorinamn: ");
            var name = Console.ReadLine();

            try
            {
                _categoryService.AddCategory(name);
                Console.WriteLine("Kategorin skapad");
                Console.ReadLine();
                AdminMenu.RenderAdminMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"gick ej att skapa produkten pga: {ex.Message}");
                return;
            }

        }







        public static void AddCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Du måste ange ett kategorinamn");
            }
            if (_database.Categories.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                throw new ArgumentException("Kategorin finns redan");
            }

            _database.Categories.Add(new Category { Name = name });
            _database.SaveChanges();
        }
    }
}
