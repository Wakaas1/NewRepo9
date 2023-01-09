using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.Domain.User;

namespace YSManagmentSystem.web.Models.DataTable
{
    public class DataTableResponse<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
        public string error { get; set; }

        public static implicit operator DataTableResponse<T>(DataTableResponse<SupplierDetail> v)
        {
            throw new NotImplementedException();
        }
    }
}
