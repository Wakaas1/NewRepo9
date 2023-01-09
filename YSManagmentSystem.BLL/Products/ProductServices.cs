using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.DataTableUser;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.Domain.User;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IDapperRepo _dapper;
       
        public ProductServices(IDapperRepo dapper)
        {
            _dapper = dapper;
            
        }

        public int AddProduct(Product model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@ProductName", model.ProductName);
            param.Add("@Description", model.Description);
            param.Add("@Quantity", model.Quantity);
            param.Add("@Image", model.Image);
            param.Add("@Price", model.Price);
            param.Add("@ProductCode", model.ProductCode);
            param.Add("@CategoryId", model.CategoryId);
            param.Add("@LocationId", model.LocationId);
            param.Add("@BrandId", model.BrandId);
            param.Add("@SupplierId", model.SupplierId);
            param.Add("@CreatedAt", model.CreatedAt);
            param.Add("@UpdatedAt", model.UpdatedAt);
            var result = _dapper.CreateUserReturnInt("dbo.AddProduct", param);
            if (result > 0)
            { }
            return result;
        }

        public int UpdateProduct(Product model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@ProductName", model.ProductName);
            param.Add("@Description", model.Description);
            param.Add("@Quantity", model.Quantity);
            param.Add("@Image", model.Image);
            param.Add("@Price", model.Price);
            param.Add("@ProductCode", model.ProductCode);
            param.Add("@CategoryId", model.CategoryId);
            param.Add("@LocationId", model.LocationId);
            param.Add("@BrandId", model.BrandId);
            param.Add("@SupplierId", model.SupplierId);
            param.Add("@CreatedAt", model.CreatedAt);
            param.Add("@UpdatedAt", model.UpdatedAt);
            var result = _dapper.CreateUserReturnInt("dbo.UpdateProduct", param);
            if (result > 0)
            { }
            return result;
        }

        public Product GetProductByID(int Id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            var pro = _dapper.ReturnList<Product>("dbo.GetProductsByID", param).FirstOrDefault();

            return pro;
        }
        //public Product GetProductByCategory(int Id)
        //{

        //    Dapper.DynamicParameters param = new DynamicParameters();
        //    param.Add("@Id", Id);
        //    var pro = _dapper.ReturnList<Product>("dbo.GetProductByID", param).FirstOrDefault();

        //    return pro;
        //}
        public int UpadateProductImage(string image, int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@Image", image);
            var result = _dapper.CreateUserReturnInt("dbo.UpdateProductImage", param);
            if (result > 0)
            { }
            return result;
        }
        public int DeleteProductImage(Product model)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@Image", model.Image);
            var result = _dapper.CreateUserReturnInt("dbo.DeleteProductImage", param);
            if (result > 0)
            { }
            return result;
        }
        public int DeleteProduct(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var pro = _dapper.CreateUserReturnInt("dbo.DeleteProduct", param);

            return pro;
        }
        public void UpdateProductQuantity(int id, int q)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@Quantity",q);
            _dapper.Execute("dbo.UpdateProductQuantity", param);

            
        }
        //public IEnumerable<ProductCategoryPartial> ProductListId(int id)
        //{
        //    Dapper.DynamicParameters param = new DynamicParameters();
        //    param.Add("@Id", id);
        //    return _dapper.ReturnList<ProductCategoryPartial>("dbo.GetProductCategoryById", param);
        //}

        public IEnumerable<ProductDetail> GetAllProducts()
        {

            var pro = _dapper.ReturnList<ProductDetail>("dbo.GetAllProducts").ToList();
            return pro;
        }
        public IEnumerable<Category> GetAllCategory()
        {

            var pro = _dapper.ReturnList<Category>("dbo.GetAllCategory").ToList();
            return pro;
        }
        public IEnumerable<Supplier> GetAllSupplier()
        {

            var pro = _dapper.ReturnList<Supplier>("dbo.GetAllSupplier").ToList();
            return pro;
        }
        public IEnumerable<Brand> GetAllBrand()
        {

            var pro = _dapper.ReturnList<Brand>("dbo.GetAllBrand").ToList();
            return pro;
        }
        public IEnumerable<Locations> GetAllLocation()
        {

            var pro = _dapper.ReturnList<Locations>("dbo.GetAllLocation").ToList();
            return pro;
        }

        public async Task<DataTableResponse<ProductDetail>> GetAllProductDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);
            param.Add("CategoryId", request.CategoryId, DbType.Int32);
            param.Add("LocationId", request.LocationId, DbType.Int32);

            var pro =  _dapper.ReturnProductListMultiple("GetAllProductDT", param);
            var Response = new DataTableResponse<ProductDetail>()
            {
                draw = request.draw,
                data = pro.Result.Rec.ToList(),
                recordsFiltered = pro.Result.TotalRecord,
                recordsTotal = pro.Result.TotalRecord,
                
            };
            return Response;
        }

    }
}
