using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Order
{
    public class Areas
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int SalePersonId { get; set; }
        public int DriverId { get; set; }
    }
}
