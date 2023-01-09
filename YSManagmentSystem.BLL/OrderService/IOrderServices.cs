using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.OrderService
{
    public interface IOrderServices
    {
       
        int AddOrder(tbl_Order model);
        int UpdateOrder(tbl_Order model);
        tbl_Order GetOrderByID(int id);
        int DeleteOrder(int id);
        IEnumerable<OrderItemList> GetOrderByItem(int id);
        int AddOrderItem(OrderItem model);
        int CreateNewOrder(int id, string st);
        List<tbl_Order> GetAllOrders();
        Task<DataTableResponse<OrderList>> GetAllOrderDT(DTReq request);
         OrderItem GetOrderItemByID(int id);
        tbl_Order AddCustomer(int id, int cid);
        void UpdateQuantity(int id, int Quantity, float Total);
        public void DeleteOrderItemList(int id);
        void AddTotal(int id, float total);
        void AddGrandTotal(int id, float tax, float dc);
        void Checkout(int id, int CId, int DId, int SId, int AId);
    }
}