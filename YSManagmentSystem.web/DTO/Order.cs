using YSManagmentSystem.Domain.Order;

namespace YSManagmentSystem.web.DTO
{
    public class Order
    {
        public List<OrderItemList> Item { get; set; }
        public int OrderId { get; set; }
        public float SubTotal { get; set; }
        public float Tax { get; set; }
        public float DeliveryCharges { get; set; }
        public float GrandTotal { get; set; }

    }
    public class AddOrderItem
    {
        public int ProductId { get; set; }
        public int Quaintity { get; set; }
        
    }
}
