using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.Domain.Product;

namespace YSManagmentSystem.Domain.Items
{
    public class ItemDetail
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string OrdersName { get; set; }
        public string SKU { get; set; }
        public float MRP { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public int Available { get; set; }
        public int Defective { get; set; }

    }

    public class TotalItem
    { public int TotalCount { get; set; } }

    public class ResultItem
    {
        public int TotalRecord { get; set; }
        public List<ItemDetail> Rec { get; set; }
    }
}
