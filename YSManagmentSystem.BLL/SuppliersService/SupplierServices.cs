using Dapper;
using System.Data;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.SuppliersService
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IDapperRepo _dapper;
        public SupplierServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

        public int AddSupplier(Supplier model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@SupplierName", model.SupplierName);
            param.Add("@Email", model.Email);
            param.Add("@Address", model.Address);
            param.Add("@ContactNumber", model.ContactNumber);
            param.Add("@BrandId", model.BrandId);
            var result = _dapper.CreateUserReturnInt("dbo.AddSupplier", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateSupplier(Supplier model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@SupplierName", model.SupplierName);
            param.Add("@Email", model.Email);
            param.Add("@Address", model.Address);
            param.Add("@ContactNumber", model.ContactNumber);
            param.Add("@BrandId", model.BrandId);
            var result = _dapper.CreateUserReturnInt("dbo.UpdateSupplier", param);
            if (result > 0)
            { }
            return result;
        }
        public Supplier GetSupplierByID(int Id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            var sup = _dapper.ReturnList<Supplier>("dbo.GetSupplierByID", param).FirstOrDefault();

            return sup;
        }
        public int DeleteSupllier(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var sup = _dapper.CreateUserReturnInt("dbo.DeleteSupplier", param);

            return sup;
        }
        public async Task<DataTableResponse<SupplierDetail>> GetAllSupplierDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);
            param.Add("BrandId", request.BrandId, DbType.Int32);

            var sup = _dapper.ReturnSupplierListMultiple("GetAllSupplierDT", param);
            var Response = new DataTableResponse<SupplierDetail>()
            {
                draw = request.draw,
                data = sup.Result.Rec,
                recordsFiltered = sup.Result.TotalRecord,
                recordsTotal = sup.Result.TotalRecord,

            };
            return Response;
        }
    }
}
