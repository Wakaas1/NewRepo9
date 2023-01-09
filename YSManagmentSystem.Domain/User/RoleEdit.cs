using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.User
{
    public class RoleEdit
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Checked { get; set; }
       
    }

    public class BrandsEdit
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public bool Checked { get; set; }

        
    }
}
