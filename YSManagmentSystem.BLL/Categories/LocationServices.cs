using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Categories
{
    public class LocationServices : ILocationServices
    {
        private readonly IDapperRepo _dapper;
        public LocationServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

      
        public int AddLocation(Locations model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Location", model.Location);

            var result = _dapper.CreateUserReturnInt("dbo.AddLocation", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateLocation(Locations model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@Location", model.Location);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateLocation", param);
            if (result > 0)
            { }
            return result;
        }
        public Locations GetLocationByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var loc = _dapper.ReturnList<Locations>("dbo.GetLocationByID", param).FirstOrDefault();

            return loc;
        }
        public int DeleteLocation(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var loc = _dapper.CreateUserReturnInt("dbo.DeleteLocation", param);

            return loc;
        }

        // DataTable, paging Sorting Searching
        public async Task<DataTableResponse<LocationDetail>> GetAllLocationDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);

            var loc = _dapper.ReturnLocationListMultiple("GetAllLocationDT", param);
            var Response = new DataTableResponse<LocationDetail>()
            {
                draw = request.draw,
                data = loc.Result.Rec,
                recordsFiltered = loc.Result.TotalRecord,
                recordsTotal = loc.Result.TotalRecord,

            };
            return Response;
        }
    }
}
