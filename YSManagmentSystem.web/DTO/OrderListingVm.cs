using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.Domain.Product;

namespace YSManagmentSystem.web.DTO
{
    public class OrderListingVm
    {
        public int OrderId { get; set; }
        public float TotalCost { get; set; }
        public float Tax { get; set; }
        public float DeliveryCharges { get; set; }
        public float GrandTotal { get; set;}
        
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<OrderItemList> OrderItems { get; set; }
    }
    
}
