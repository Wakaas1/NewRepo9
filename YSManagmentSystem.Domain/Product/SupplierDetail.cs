using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Product
{
    public class SupplierDetail
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string BrandName { get; set; }
    }
    public class Totalsup
    { public int TotalCount { get; set; } }

    public class Resultsup
    {
        public int TotalRecord { get; set; }
        public List<SupplierDetail> Rec { get; set; }
    }
}
