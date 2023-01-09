using YSManagmentSystem.Domain.Order;

namespace YSManagmentSystem.BLL.OrderService
{
    public interface ISalePersonsServices
    {
        int AddArea(Areas model);
        int AddSalePerson(SalePersons model);
        int DeleteArea(int id);
        int DeleteSalePerson(int id);
        List<Areas> GetAllAreas();
        List<SalePersons> GetAllSalePersons();
        Areas GetAreaByID(int id);
        SalePersons GetSalePersonByID(int id);
        int UpdateArea(Areas model);
        int UpdateSalePerson(SalePersons model);
        int GetDriverArea(int id);
    }
}