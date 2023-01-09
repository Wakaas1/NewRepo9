using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Categories
{
    public interface ICategoryServices
    {
        int AddCategory(Category model);
        int DeleteCategory(int id);
     
        Category GetCategoryByID(int Id);
        int UpdateCategory(Category model);
        Task<DataTableResponse<CategoryDetail>> GetAllCategoryDT(DTReq request);
    }
}