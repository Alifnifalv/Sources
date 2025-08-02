using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentTransportDetailViewModel
    {
        public long StudentID { get; set; }
        public string Name { get; set; }       
        public string PickupStopMapName { get; set; }        
        public string DropStopMapName { get; set; }
        public string DropStopDriverName { get; set; }
        public string DropStopRouteCode { get; set; }
        public string PickupRouteCode { get; set; }
        public string PickupContactNo { get; set; }
        public string DropContactNo { get; set; }
        public string PickupStopDriverName { get; set; }
        public Boolean IsOneWay { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public long ClassID { get; set; }
        public long SectionID { get; set; }
        public long LoginID { get; set; }
        public long StudentRouteStopMapIID { get; set; }
    }
}
