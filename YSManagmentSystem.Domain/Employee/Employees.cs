using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Employee
{
    public class Employees
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public int CnicNumber { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Experience { get; set; }
        public string BrandAssigned { get; set; }
        public DateTime DateOfjoining { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string PreviousDetail { get; set; }
        public int BrandId { get; set; }
        public string ReferredBy { get; set; }
        public string Image { get; set; }

    }
}
