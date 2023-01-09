namespace YSManagmentSystem.web.DTO
{
    //public class AddOrderItem
    //{
       
    //    public int ProductId { get; set; }
    //    public float Cost { get; set; }
    //    public int Quaintity { get; set; }
    //    public float total { get { return Cost * Quaintity; } }
    //}
    public class OrderId
    {
        public int Id { get; set; } = 0;
    }
    public class orderTotal
    {
        public float Total { get; set; }
        public float Tax { get; set; }
        public float DeliveryCharges { get; set; }
        public float Discount { get; set; }
        public float GrandTotal { get; set; }
    } 
    public class AddOrder
    {   public int Id { get; set; }
        public int ProductId { get; set;}
        public string Detail { get; set; }
        public int Quantity { get; set; }
    }
}
