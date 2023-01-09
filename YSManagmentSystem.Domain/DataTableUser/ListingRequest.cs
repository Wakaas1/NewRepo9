namespace YSManagmentSystem.web.Models.DataTable
{
    public class ListingRequest
    {
        public string SearchValue { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SortColumn { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
