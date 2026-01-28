using Ikea.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.InUseClasses
{
    public class AdminManagement
    {
        private readonly MyDBContext _database;

        public AdminManagement()
        {
            _database = new MyDBContext();
        }

        public async Task RenderAdminAllOrders()
        {
            var sw = new Stopwatch();
            sw.Start();
            var order = await _database.Orders.ToListAsync();


            sw.Stop();
            Console.WriteLine($"Hämtningen tog {sw.ElapsedMilliseconds} millisekunder");
        }
    }
}
