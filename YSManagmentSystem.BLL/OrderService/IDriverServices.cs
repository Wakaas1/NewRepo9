using YSManagmentSystem.Domain.Order;

namespace YSManagmentSystem.BLL.OrderService
{
    public interface IDriverServices
    {
        int AddDriver(Driver model);
        int AddVehicle(VehicleDetail model);
        int DeleteDriver(int id);
        int DeleteVehicle(int id);
        Driver GetDriverByID(int id);
        VehicleDetail GetVehicleByID(int id);
        int UpdateDriver(Driver model);
        int UpdateVehicle(VehicleDetail model);
        List<Driver> GetAllDrivers();
        List<VehicleDetail> GetAllVehicles();
    }
}