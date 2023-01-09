using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Product
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class CategoryDetail
    {
        public int Id { get; set; }
        public string CategoryName { get; set;} 
        public string Description { get; set;}
    }
    public class TotalCat
    { public int TotalCount { get; set; } }

    public class ResultCat
    {
        public int TotalRecord { get; set; }
        public List<CategoryDetail> Rec { get; set; }
    }
}
