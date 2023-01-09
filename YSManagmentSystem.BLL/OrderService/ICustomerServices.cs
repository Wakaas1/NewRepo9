using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.OrderService
{
    public interface ICustomerServices
    {
        int AddCustomer(Customer model);
        int DeleteCustomer(int id);
        Task<DataTableResponse<CustomerDetail>> GetAllCustomerDT(DTReq request);
        Customer GetCustomerByID(int id);
        List<Customer> GetAllCustomer();
        int UpdateCustomer(Customer model);
    }
}