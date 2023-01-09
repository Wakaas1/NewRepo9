using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Order;

namespace YSManagmentSystem.BLL.OrderService
{
    public class SalePersonsServices : ISalePersonsServices
    {
        private readonly IDapperRepo _dapper;
        public SalePersonsServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

        public List<SalePersons> GetAllSalePersons()
        {
            var loc = _dapper.ReturnList<SalePersons>("dbo.GetAllSalePerson").ToList();

            return loc;
        }
        public int AddSalePerson(SalePersons model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@SalePersonName", model.SalePersonName);
            param.Add("@ContactNumber", model.ContactNumber);
            param.Add("@AreaId", model.AreaId);

            var result = _dapper.CreateUserReturnInt("dbo.AddSalePerson", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateSalePerson(SalePersons model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@SalePersonName", model.SalePersonName);
            param.Add("@ContactNumber", model.ContactNumber);
            param.Add("@AreaId", model.AreaId);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateSalePerson", param);
            if (result > 0)
            { }
            return result;
        }
        public SalePersons GetSalePersonByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var loc = _dapper.ReturnList<SalePersons>("dbo.GetSalePersonById", param).FirstOrDefault();

            return loc;
        }
        public int DeleteSalePerson(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var loc = _dapper.CreateUserReturnInt("dbo.DeleteSalePerson", param);

            return loc;
        }


        // DataTable, paging Sorting Searching
        //public async Task<DataTableResponse<LocationDetail>> GetAllLocationDT(DTReq request)
        //{
        //    Dapper.DynamicParameters param = new DynamicParameters();
        //    param.Add("SearchText", request.SearchText, DbType.String);
        //    param.Add("SortExpression", request.SortExpression, DbType.String);
        //    param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
        //    param.Add("PageSize", request.PageSize, DbType.Int32);

        //    var loc = _dapper.ReturnLocationListMultiple("GetAllLocationDT", param);
        //    var Response = new DataTableResponse<LocationDetail>()
        //    {
        //        draw = request.draw,
        //        data = loc.Result.Rec,
        //        recordsFiltered = loc.Result.TotalRecord,
        //        recordsTotal = loc.Result.TotalRecord,

        //    };
        //    return Response;
        //}

        public List<Areas> GetAllAreas()
        {
            var loc = _dapper.ReturnList<Areas>("dbo.GetAllArea").ToList();

            return loc;
        }
        public int AddArea(Areas model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@AreaName", model.AreaName);
            param.Add("@SalePersonId", model.SalePersonId);

            var result = _dapper.CreateUserReturnInt("dbo.AddArea", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateArea(Areas model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@AreaName", model.AreaName);
            param.Add("@SalePersonId", model.SalePersonId);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateArea", param);
            if (result > 0)
            { }
            return result;
        }
        public Areas GetAreaByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var loc = _dapper.ReturnList<Areas>("dbo.GetAreaById", param).FirstOrDefault();

            return loc;
        }
        public int DeleteArea(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var loc = _dapper.CreateUserReturnInt("dbo.DeleteArea", param);

            return loc;
        }
        public int GetDriverArea(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var loc = _dapper.CreateUserReturnInt("dbo.GetDriverSalePersonArea", param);

            return loc;
        }
    }
}
