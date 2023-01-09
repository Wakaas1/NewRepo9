using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Categories
{
    public interface ILocationServices
    {
        int AddLocation(Locations model);
        int DeleteLocation(int id);
        
        Locations GetLocationByID(int Id);
        int UpdateLocation(Locations model);
        Task<DataTableResponse<LocationDetail>> GetAllLocationDT(DTReq request);

    }
}