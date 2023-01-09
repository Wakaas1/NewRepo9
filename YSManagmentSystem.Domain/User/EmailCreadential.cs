using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.User
{
    public class EmailCreadential
    {
        public int Id { get; set; }
        public string EmailTemp { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } 
    }
}
