using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Product
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Quantity { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public string BrandName { get; set; }
        public string Location { get; set; }
        
    }
    public class Totalpro
    { public int TotalCount { get; set; } }

    public class ResultPro
    { public int TotalRecord { get; set; }
        public List<ProductDetail> Rec { get;set; }
    }
}
