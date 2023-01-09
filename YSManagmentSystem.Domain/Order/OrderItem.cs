using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using YSManagmentSystem.Domain.Product;

namespace YSManagmentSystem.Domain.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Cost { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }

       
    }
    public class OrderItemList
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public float Price { get; set; }
            public float Total { get; set; }
        }

    
 
}
