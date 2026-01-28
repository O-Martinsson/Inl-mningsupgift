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
    }
}
