using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.Domain.DataTableUser;
using YSManagmentSystem.Domain.Items;
using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;


namespace YSManagmentSystem.DAL.Data
{
    public class DapperRepo : IDapperRepo
    {
        private readonly string connectionString;

        public DapperRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Default");
        }


        public T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.Execute(procrdureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }

        }
        public void Execute(string procrdureName, DynamicParameters param)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procrdureName, param, commandType: CommandType.StoredProcedure);
            }

        }

        //DapperORM.RetrunList<EmployeeModel> <= IEnumberable<EmployeeModel>

        public IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procrdureName, param, commandType: CommandType.StoredProcedure);
            }

        }
        public int CreateUserReturnInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("Id");
            }
        }

        public int CreateUserReturnFKInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("UId");
            }
        }
        public int CreateRoleReturnInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("RoleId");
            }
        }
        public int CreateProductReturnFKInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("P_Id");
            }
        }
        public async Task<ResultUser> ReturnListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultUser();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.UserRec = query.Read<UserDetailDT>().AsList<UserDetailDT>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }
        public async Task<ResultUserBrand> ReturnUBListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultUserBrand();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.UserbrandRec = query.Read<UserBrandDetailDT>().AsList<UserBrandDetailDT>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }
        public async Task<ResultPro> ReturnProductListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultPro();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<ProductDetail>().AsList<ProductDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }

        public async Task<Resultsup> ReturnSupplierListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new Resultsup();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<SupplierDetail>().AsList<SupplierDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }

        public async Task<ResultCat> ReturnCategoryListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultCat();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<CategoryDetail>().AsList<CategoryDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }

        public async Task<Resultbrand> ReturnBrandListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new Resultbrand();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<BrandDetail>().AsList<BrandDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }
        public async Task<ResultLoc> ReturnLocationListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultLoc();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<LocationDetail>().AsList<LocationDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }

        public async Task<ResultItem> ReturnItemListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultItem();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<ItemDetail>().AsList<ItemDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }

        public async Task<ResultOrder> ReturnOrderListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultOrder();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<OrderList>().AsList<OrderList>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }


        public async Task<ResultCustomer> ReturnCustomerListMultiple(string procrdureName, DynamicParameters param = null)
        {
            var res = new ResultCustomer();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = await sqlCon.QueryMultipleAsync(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    res.Rec = query.Read<CustomerDetail>().AsList<CustomerDetail>();
                    if (!query.IsConsumed)
                        res.TotalRecord = query.Read<int>().FirstOrDefault();
                }
            }
            return res;
        }

      
    }
}
