using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Product
{
    public class Product
    {
        public int Id { get; set; } 
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public int LocationId { get; set; }
        public int SupplierId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public int BrandId { get; set; }
    }
}
