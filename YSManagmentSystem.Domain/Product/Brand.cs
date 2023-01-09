using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Product
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }

    public class BrandDetail
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
    public class Totalbrand
    { public int TotalCount { get; set; } }

    public class Resultbrand
    {
        public int TotalRecord { get; set; }
        public List<BrandDetail> Rec { get; set; }
    }
}
