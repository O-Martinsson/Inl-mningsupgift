using Ikea.Models;
using Ikea.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    
    public class AdminCategoryEditor
    {
        private static MyDBContext _database = new MyDBContext();
        private static CategoryService _categoryService = new CategoryService(_database);
        
        public static void RenderEditCategory()
        {
            Console.Clear();

            var categories = _categoryService.GetAllCategories();
            foreach (var c in categories)
            {
                Console.WriteLine($"{c.Id}. {c.Name}");
            }
            Console.Write("Välj kategoriId");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Error, Try again");
                return;
            }

            Console.WriteLine("Ange nytt namn: ");
            var name = Console.ReadLine();

            try
            {
                _categoryService.Update(id, name);
                Console.WriteLine("Kategorin är uppdaterad");
                Console.ReadLine();
                AdminMenu.RenderAdminMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error msg {ex.Message}");
            }


        }
    }
}
