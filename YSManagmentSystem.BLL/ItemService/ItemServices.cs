using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Items;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.ItemService
{
    public class ItemServices : IItemServices
    {
        private readonly IDapperRepo _dapper;
        public ItemServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

        public int AddItem(Item model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@productId", model.productId);
            param.Add("@brandId", model.brandId);
            param.Add("@supplierId", model.supplierId);
            param.Add("@orderId", model.orderId);
            param.Add("@sku", model.sku);
            param.Add("@mrp", model.mrp);
            param.Add("@discount", model.discount);
            param.Add("@price", model.price);
            param.Add("@quantity", model.quantity);
            param.Add("@sold", model.sold);
            param.Add("@available", model.available);
            param.Add("@defective", model.defective);
            param.Add("@createdBy", model.createdBy);
            param.Add("@updatedBy", model.updatedBy);
            param.Add("@createdAt", model.createdAt);
            param.Add("@updatedAt", model.updatedAt);

            var result = _dapper.CreateUserReturnInt("dbo.AddItem", param);
            if (result > 0)
            { }
            return result;
        }

        public int UpdateItem(Item model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@productId", model.productId);
            param.Add("@brandId", model.brandId);
            param.Add("@supplierId", model.supplierId);
            param.Add("@orderId", model.orderId);
            param.Add("@sku", model.sku);
            param.Add("@mrp", model.mrp);
            param.Add("@discount", model.discount);
            param.Add("@price", model.price);
            param.Add("@quantity", model.quantity);
            param.Add("@sold", model.sold);
            param.Add("@available", model.available);
            param.Add("@defective", model.defective);
            param.Add("@createdBy", model.createdBy);
            param.Add("@updatedBy", model.updatedBy);
            param.Add("@createdAt", model.createdAt);
            param.Add("@updatedAt", model.updatedAt);
            var result = _dapper.CreateUserReturnInt("dbo.UpdateItem", param);
            if (result > 0)
            { }
            return result;
        }

        public Item GetItemByID(int Id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            var pro = _dapper.ReturnList<Item>("dbo.GetItemById", param).FirstOrDefault();

            return pro;
        }

        public int DeleteItem(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var pro = _dapper.CreateUserReturnInt("dbo.DeleteItem", param);

            return pro;
        }

        public IEnumerable<ItemDetail> GetAllItems()
        {

            var itm = _dapper.ReturnList<ItemDetail>("dbo.GetAllProducts").ToList();
            return itm;
        }
        public IEnumerable<ProductDetail> GetAllProducts()
        {

            var pro = _dapper.ReturnList<ProductDetail>("dbo.GetAllProducts").ToList();
            return pro;
        }

        public IEnumerable<Supplier> GetAllSupplier()
        {

            var sup = _dapper.ReturnList<Supplier>("dbo.GetAllSupplier").ToList();
            return sup;
        }
        public IEnumerable<Brand> GetAllBrand()
        {

            var bnd = _dapper.ReturnList<Brand>("dbo.GetAllBrand").ToList();
            return bnd;
        }
        public IEnumerable<Locations> GetAllOrder()
        {

            var loc = _dapper.ReturnList<Locations>("dbo.GetAllOrder").ToList();
            return loc;
        }

        public async Task<DataTableResponse<ItemDetail>> GetAllItemDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);
            param.Add("productId", request.productId, DbType.Int32);
            param.Add("brandId", request.BrandId, DbType.Int32);
            param.Add("orderId", request.orderId, DbType.Int32);
            param.Add("supplierId", request.supplierId, DbType.Int32);

            var pro = _dapper.ReturnItemListMultiple("GetAllItemDT", param);
            var Response = new DataTableResponse<ItemDetail>()
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
