using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Order
{
    public  class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }

    public class CustomerDetail
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }

    }
    public class TotalCustomer
    { public int TotalCount { get; set; } }

    public class ResultCustomer
    {
        public int TotalRecord { get; set; }
        public List<CustomerDetail> Rec { get; set; }
    }

}
