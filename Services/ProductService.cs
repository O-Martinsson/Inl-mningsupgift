using Ikea.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.Services
{
    public class ProductService
    {
        private readonly MyDBContext _database;

        public ProductService(MyDBContext mydbcontext)
        {
            _database = mydbcontext;
        }

        public List<Product> GetAllProducts()
        {
            return _database.Products
                .Include(p => p.Category)
                .ToList();
        }

        public List<Product> GetSpecialOfferProducts(int take = 3)
        {
            return _database.Products
                .Include(p => p.Category)
                .Where(x => x.IsOnSale)
                .Take(take)
                .ToList();
        }

        public void UpdateProduct(int productId, string name, string description, string priceInput, string offerInput)
        {
            var product = _database.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null)
            {
                throw new ArgumentException($"Produkten hittades inte i databasen med ProduktId {productId}");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                product.Name = name;
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                product.Description = description;
            }

            // Pris
            if (!string.IsNullOrWhiteSpace(priceInput))
            {
                if (decimal.TryParse(priceInput, out var price) && price > 0)
                {
                    product.Price = price;
                }
                else
                {
                    throw new ArgumentException("Felaktigt pris, priset ändras inte.");
                }
            }

            //specialerbjudande
            if (!string.IsNullOrWhiteSpace(offerInput))
            {
                if (offerInput.ToLower() == "j")
                {
                    product.IsOnSale = true;
                }
                else if (offerInput.ToLower() == "n")
                {
                    product.IsOnSale = false;
                }
            }
            _database.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            _database.Products.Remove(product);
            _database.SaveChanges();
        }

        public void CreateProduct(Product product)
        {
            //Validering
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Produktnamn saknas");
            }
            _database.Products.Add(product);
            _database.SaveChanges();



        }
    }
}
