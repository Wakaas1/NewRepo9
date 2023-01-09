using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Categories
{
    public interface IBrandServices
    {
        int AddBrand(Brand model);
        int DeleteBrand(int id);
        
        Brand GetBrandByID(int Id);
        int UpdateBrand(Brand model);
        Task<DataTableResponse<BrandDetail>> GetAllBrandDT(DTReq request);
    }
}