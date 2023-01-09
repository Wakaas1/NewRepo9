using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.OrderService
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IDapperRepo _dapper;
        public CustomerServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }


        public int AddCustomer(Customer model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@CustomerName", model.CustomerName);
            param.Add("@CustomerAddress", model.CustomerAddress);
            param.Add("@Email", model.Email);
            param.Add("@ContactNumber", model.ContactNumber);

            var result = _dapper.CreateUserReturnInt("dbo.AddCustomer", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateCustomer(Customer model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@CustomerName", model.CustomerName);
            param.Add("@CustomerAddress", model.CustomerAddress);
            param.Add("@Email", model.Email);
            param.Add("@ContactNumber", model.ContactNumber);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateCustomer", param);
            if (result > 0)
            { }
            return result;
        }
        public Customer GetCustomerByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var cus = _dapper.ReturnList<Customer>("dbo.GetCustomerByID", param).FirstOrDefault();

            return cus;
        }

        public List<Customer> GetAllCustomer()
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            
            var cus = _dapper.ReturnList<Customer>("dbo.GetAllCustomer", param).ToList();

            return cus;
        }
        public int DeleteCustomer(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var cus = _dapper.CreateUserReturnInt("dbo.DeleteCustomer", param);

            return cus;
        }

        // DataTable, paging Sorting Searching
        public async Task<DataTableResponse<CustomerDetail>> GetAllCustomerDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);

            var cus = _dapper.ReturnCustomerListMultiple("GetAllCustomerDT", param);
            var Response = new DataTableResponse<CustomerDetail>()
            {
                draw = request.draw,
                data = cus.Result.Rec,
                recordsFiltered = cus.Result.TotalRecord,
                recordsTotal = cus.Result.TotalRecord,

            };
            return Response;
        }
    }
}

