using Dapper;
using System.Data;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Order;

namespace YSManagmentSystem.BLL.OrderService
{
    public class DriverServices : IDriverServices
    {
        private readonly IDapperRepo _dapper;
        public DriverServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

        public List<Driver> GetAllDrivers()
        {
            var loc = _dapper.ReturnList<Driver>("dbo.GetAllDriver").ToList();

            return loc;
        }

        public List<VehicleDetail> GetAllVehicles()
        {
            var loc = _dapper.ReturnList<VehicleDetail>("dbo.GetAllVehicle").ToList();

            return loc;
        }
        public int AddDriver(Driver model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Name", model.Name);
            param.Add("@ContactNumber", model.ContactNumber);
            param.Add("@VehicleId", model.VehicleId);

            var result = _dapper.CreateUserReturnInt("dbo.AddDriver", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateDriver(Driver model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@Name", model.Name);
            param.Add("@ContactNumber", model.ContactNumber);
            param.Add("@VehicleId", model.VehicleId);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateDriver", param);
            if (result > 0)
            { }
            return result;
        }
        public Driver GetDriverByID(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var loc = _dapper.ReturnList<Driver>("dbo.GetDriverById", param).FirstOrDefault();

            return loc;
        }
        public int DeleteDriver(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var loc = _dapper.CreateUserReturnInt("dbo.DeleteDriver", param);

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

        public int AddVehicle(VehicleDetail model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@VehicleName", model.VehicleName);
            param.Add("@RegistrationNumber", model.RegistrationNumber);
            param.Add("@Model", model.Model);

            var result = _dapper.CreateUserReturnInt("dbo.AddVehicle", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateVehicle(VehicleDetail model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@VehicleName", model.VehicleName);
            param.Add("@RegistrationNumber", model.RegistrationNumber);
            param.Add("@Model", model.Model);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateVehicle", param);
            if (result > 0)
            { }
            return result;
        }
        public VehicleDetail GetVehicleByID(int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var loc = _dapper.ReturnList<VehicleDetail>("dbo.GetVehicleById", param).FirstOrDefault();

            return loc;
        }
        public int DeleteVehicle(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var loc = _dapper.CreateUserReturnInt("dbo.DeleteVehicle", param);

            return loc;
        }
    }
}
