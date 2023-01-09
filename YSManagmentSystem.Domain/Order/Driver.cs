using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Order
{
    public  class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string SalePersonId { get; set; }
        public int VehicleId { get; set; }
        public int AreaId { get; set; }
    }
}
