using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.BLL.Categories;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.OrderService
{
    public class OrderServices : IOrderServices
    {
        private readonly IDapperRepo _dapper;
        public OrderServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

      
        public int AddOrder(tbl_Order model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@OrderNumber",model.OrderNumber);
            param.Add("@CustomerId", model.CustomerId);
            param.Add("@Status", model.Status);
            param.Add("@OrderDate", model.OrderDate);
          


            var result = _dapper.CreateUserReturnInt("dbo.AddOrder", param);
            if (result > 0)
            { }
            return result;
        }

        public int AddOrderItem(OrderItem model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@OrderId", model.OrderId);
            param.Add("@ProductId", model.ProductId);
            param.Add("@Cost", model.Cost);
            param.Add("@Quantity", model.Quantity);
            param.Add("@Discount", model.Discount);
            param.Add("@Total", model.Total);


            var result = _dapper.CreateUserReturnInt("dbo.AddOrderItem", param);
            if (result > 0)
            { }
            return result;
        }

        
        public int UpdateOrder(tbl_Order model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@OrderNumber", model.OrderNumber);
            param.Add("@CustomerId", model.CustomerId);
            param.Add("@Status", model.Status);
            param.Add("@OrderDate", model.OrderDate);
            param.Add("@CompletedDate", model.CompletedDate);
            param.Add("@TotalCost", model.TotalCost);
            param.Add("@Tax", model.Tax);
            param.Add("@DeliveryCharges", model.DeliveryCharges);
            param.Add("@Total", model.Total);


            var result = _dapper.CreateUserReturnInt("dbo.UpdateOrder", param);
            if (result > 0)
            { }
            return result;
        }
        public OrderItem GetOrderItemByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var ord = _dapper.ReturnList<OrderItem>("dbo.GetOrderItemById", param).FirstOrDefault();

            return ord;
        }
        public tbl_Order GetOrderByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var ord = _dapper.ReturnList<tbl_Order>("dbo.GetOrderByID", param).FirstOrDefault();

            return ord;
        }
        public tbl_Order AddCustomer(int id,int cid)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@CId", cid);
            var ord = _dapper.ReturnList<tbl_Order>("dbo.AddCustomerToOrder", param).FirstOrDefault();

            return ord;
        }
        public int DeleteOrder(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var ord = _dapper.CreateUserReturnInt("dbo.DeleteOrder", param);

            return ord;
        }
        public IEnumerable<OrderItemList> GetOrderByItem(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@OrderId", id);
            var ord = _dapper.ReturnList<OrderItemList>("dbo.GetOrderItem", param);

            return ord;
        }

        public List<tbl_Order> GetAllOrders()
        {

            Dapper.DynamicParameters param = new DynamicParameters();
           
            var ord = _dapper.ReturnList<tbl_Order>("dbo.GetAllOrderList", param).ToList();

            return ord;
        }
        public int CreateNewOrder(int id,string st)
        {
         
            var order = new tbl_Order();
            order.OrderDate = DateTime.Now;
            order.OrderNumber =  st;
            order.CustomerId = id;
            order.Status = false;
            return AddOrder(order);
        }
        // DataTable, paging Sorting Searching
        public async Task<DataTableResponse<OrderList>> GetAllOrderDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);

            var ord = _dapper.ReturnOrderListMultiple("GetAllOrderDT", param);
            var Response = new DataTableResponse<OrderList>()
            {
                draw = request.draw,
                data = ord.Result.Rec,
                recordsFiltered = ord.Result.TotalRecord,
                recordsTotal = ord.Result.TotalRecord,

            };
            return Response;
        }


        //Update Quantity

        public void UpdateQuantity(int id, int Quantity, float Total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Quantity", Quantity);
            parameters.Add("@Total", Total);

            _dapper.Execute("UpdateItemQuantity", parameters);
        }

        public void DeleteOrderItemList(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            _dapper.Execute("DeleteOrderItemList", parameters);
        }
        public void UpdateOrderNumber(int id,string OR)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@OrderNumber", OR);
            _dapper.Execute("DeleteOrderItemList", parameters);
        }

        public void AddTotal(int id,float total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Total", total);
            _dapper.Execute("AddTotal", parameters);
        }
        public void AddGrandTotal(int id, float tax, float dc)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Tax", tax);
            parameters.Add("@Dc", dc);
            _dapper.Execute("AddTaxandDc", parameters);
        }

        public void Checkout(int id, int CId, int DId, int SId, int AId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@CID", CId);
            parameters.Add("@DID", DId);
            parameters.Add("@SID", SId);
            parameters.Add("@AID", AId);
            _dapper.Execute("Checkout", parameters);
        }
    }
}
