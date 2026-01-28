using Ikea.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.Services
{
    internal class CategoryService
    {
        
        private readonly MyDBContext _database;
        public CategoryService(MyDBContext mydbcontext)
        {
            _database = mydbcontext;
        }

        public List<Category> GetAllCategories()
        { 
            return _database.Categories.Include(
                x => x.Products).ToList();
        }
        public void AddCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
            { 
                throw new ArgumentNullException("kategorinamn saknas!");
            }
            if (_database.Categories.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                throw new ArgumentException("Kategori finns redan");
            }

            _database.Categories.Add(new Category { Name = name });
            _database.SaveChanges();
        }

        public Category GetCategoryId(int id) 
        {
            return _database.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void Update(int categoryId, string NewName)
        {
            var newCategory = GetCategoryId(categoryId);
            if (newCategory == null)
            {
                throw new ArgumentException($"Det finns ingen kategori med id:{categoryId}");
            }
            newCategory.Name = NewName;
            _database.SaveChanges();
        }

    }
}
