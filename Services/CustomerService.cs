using Ikea.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.Services
{
    public class CustomerService
    {
        private readonly MyDBContext _database;
        
        public CustomerService(MyDBContext mydbcontext)
        {
            _database = mydbcontext;
        }

        public List<Customer> GetAllCustomers()
        {
            return _database.Customers.ToList();
        }
        
        public Customer GetByLogIn(string email, string password)
        {
            return _database.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.Email == email && c.Password == password);
        }

        public void CreateCustomer(string name, string email, string address, string city, string phone, string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Namn Saknas!");
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email saknas!");
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Giltig Email saknas. Försök igen");
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Telefonnummer saknas!");
            }
            if (string.IsNullOrWhiteSpace(userPassword))
            {
                throw new ArgumentException("Lösenord saknas!");
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Adress saknas!");
            }
            if (!string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("Stad saknas!");
            }

            _database.Add(new Customer
            {
                Name = name,
                Email = email,
                Address = address,
                City = city,
                Phonenumber = phone,
                Password = userPassword
            });
            _database.SaveChanges();
        }



    }
}
