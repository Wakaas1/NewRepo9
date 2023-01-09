using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSManagmentSystem.Domain.Product
{
    public class Locations
    {
        public int Id { get; set; }
        public string Location { get; set; }
    }
    public class LocationDetail
    {

        public int Id { get; set; }
        public string Location { get; set; }
    }
    public class TotalLoc
    { public int TotalCount { get; set; } }

    public class ResultLoc
    {
        public int TotalRecord { get; set; }
        public List<LocationDetail> Rec { get; set; }
    }
}
