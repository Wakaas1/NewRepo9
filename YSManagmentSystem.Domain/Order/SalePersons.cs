using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Order
{
    public class SalePersons
    {
        public int Id { get; set; }
        public string SalePersonName { get; set; }
        public string ContactNumber { get; set; }
        public int AreaId { get;set; }
    }
}
