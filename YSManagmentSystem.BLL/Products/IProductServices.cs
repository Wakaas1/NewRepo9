using YSManagmentSystem.Domain.DataTableUser;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Products
{
    public interface IProductServices
    {
        int AddProduct(Product model);
        int DeleteProduct(int id);
        int DeleteProductImage(Product model);
        IEnumerable<Brand> GetAllBrand();
        IEnumerable<Category> GetAllCategory();
        IEnumerable<Locations> GetAllLocation();
        IEnumerable<ProductDetail> GetAllProducts();
        IEnumerable<Supplier> GetAllSupplier();
        Product GetProductByID(int Id);
        int UpadateProductImage(string image, int id);
        int UpdateProduct(Product model);
        Task<DataTableResponse<ProductDetail>> GetAllProductDT(DTReq request);
        void UpdateProductQuantity(int id, int q);
    }
}