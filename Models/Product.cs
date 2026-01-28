using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category? Category { get; set; }
        public Color Color { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsOnSale { get; set; }
        public int? CategoryId { get; set; }



    }
    public enum Color
    {
        Ek,
        Vitt,
        Svart,
        Lönn,

    }
}
