using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.User
{
    public class AppRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class AppUserRole
    {
        public int RId { get; set;}
        public int UId { get; set;}
    }
}
