using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Order
{
    public class OrderListing
    {
        
        public List<OrderItemList> Items { get; set; }
        public float GrandTotal { get; set; }

    }
}
