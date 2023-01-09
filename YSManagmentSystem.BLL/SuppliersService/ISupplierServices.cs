using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.SuppliersService
{
    public interface ISupplierServices
    {
        int AddSupplier(Supplier model);
        int DeleteSupllier(int id);
        
        Supplier GetSupplierByID(int Id);
        int UpdateSupplier(Supplier model);
        Task<DataTableResponse<SupplierDetail>> GetAllSupplierDT(DTReq request);
    }
}