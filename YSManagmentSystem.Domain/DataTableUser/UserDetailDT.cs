using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.Domain.Product;

namespace YSManagmentSystem.Domain.DataTableUser
{
    public class UserDetailDT
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string roles_list { get; set; }
    }
    public class TotalUser
    { public int TotalCount { get; set; } }

    public class ResultUser
    {
        public int TotalRecord { get; set; }
        public List<UserDetailDT> UserRec { get; set; }
    }

    public class UserBrandDetailDT
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string brand_list { get; set; }
    }
    public class TotalUserBrand
    { public int TotalCount { get; set; } }

    public class ResultUserBrand
    {
        public int TotalRecord { get; set; }
        public List<UserBrandDetailDT> UserbrandRec { get; set; }
    }
}