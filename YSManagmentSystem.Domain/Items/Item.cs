using Microsoft.AspNetCore.Http.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Items
{
    public class Item
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int brandId { get; set; }
        public int supplierId { get; set; }
        public int orderId { get; set; }
        public string sku { get; set; }
        public float mrp { get; set; }
        public float discount { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public int sold { get; set; }
        public int available { get; set; }
        public int defective { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
