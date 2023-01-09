using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.Domain.Product;

namespace YSManagmentSystem.Domain.Order
{
    public class tbl_Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public bool Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public float TotalCost { get; set; }
        public float Tax { get; set; }
        public float DeliveryCharges { get; set; }
        public float Total { get; set; }
        public int DriverId { get; set; }
        public int SalePersonId { get; set; }
        public int AreaId { get; set; }
    }
    public class OrderList
    {
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public bool Status { get; set; }
        public float Total { get; set; }

    }
    public class TotalOrder
    { public int TotalCount { get; set; } }

    public class ResultOrder
    {
        public int TotalRecord { get; set; }
        public List<OrderList> Rec { get; set; }
    }
}
