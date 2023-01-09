using YSManagmentSystem.Domain.Items;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.ItemService
{
    public interface IItemServices
    {
        int AddItem(Item model);
        int DeleteItem(int id);
        IEnumerable<Brand> GetAllBrand();
        Task<DataTableResponse<ItemDetail>> GetAllItemDT(DTReq request);
        IEnumerable<Locations> GetAllOrder();
        IEnumerable<ProductDetail> GetAllProducts();
        IEnumerable<Supplier> GetAllSupplier();
        Item GetItemByID(int Id);
        int UpdateItem(Item model);
        IEnumerable<ItemDetail> GetAllItems();
    }
}