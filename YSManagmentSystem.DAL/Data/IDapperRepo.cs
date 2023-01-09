using Dapper;
using YSManagmentSystem.Domain.DataTableUser;
using YSManagmentSystem.Domain.Items;
using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.DAL.Data
{
    public interface IDapperRepo
    {
        int CreateUserReturnInt(string StoredProcedure, DynamicParameters param = null);
        void Execute(string procrdureName, DynamicParameters param);
        T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters param = null);
        IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters param = null);
        int CreateRoleReturnInt(string StoredProcedure, DynamicParameters param = null);
        int CreateUserReturnFKInt(string StoredProcedure, DynamicParameters param = null);
        Task<ResultUser> ReturnListMultiple(string procrdureName, DynamicParameters param = null);
        int CreateProductReturnFKInt(string StoredProcedure, DynamicParameters param = null);
        Task<ResultPro> ReturnProductListMultiple(string procrdureName, DynamicParameters param = null);
        Task<Resultsup> ReturnSupplierListMultiple(string procrdureName, DynamicParameters param = null);
        Task<ResultCat> ReturnCategoryListMultiple(string procrdureName, DynamicParameters param = null);
        Task<Resultbrand> ReturnBrandListMultiple(string procrdureName, DynamicParameters param = null);
        Task<ResultLoc> ReturnLocationListMultiple(string procrdureName, DynamicParameters param = null);
        Task<ResultItem> ReturnItemListMultiple(string procrdureName, DynamicParameters param = null);
        Task<ResultOrder> ReturnOrderListMultiple(string procrdureName, DynamicParameters param = null);
        Task<ResultCustomer> ReturnCustomerListMultiple(string procrdureName, DynamicParameters param = null);
        Task<ResultUserBrand> ReturnUBListMultiple(string procrdureName, DynamicParameters param = null);


    }
}