namespace YSManagmentSystem.web.Models.DataTable
{
    public class DTReq
    {
        public int draw { get; set; }
        public string SearchText { get; set; }
        public string SortExpression { get; set; }
        public int StartRowIndex { get; set; }
        public int PageSize { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public int BrandId { get; set; }
        public int productId { get; set; }
        public int supplierId { get; set; }
        public int orderId { get; set; }
       
    }
}
