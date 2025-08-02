using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentTransportDetailDTO
    {
        [DataMember]
        public long StudentID { get; set; }
        [DataMember]
        public string Name { get; set; } 
        [DataMember]
        public string AdmissionNumber { get; set; } 
        [DataMember]
        public string PickupStopMapName { get; set; }
        [DataMember]
        public string DropStopMapName { get; set; }
        [DataMember]
        public string DropStopDriverName { get; set; }
        [DataMember]
        public string DropStopRouteCode { get; set; }
        [DataMember]
        public string PickupRouteCode { get; set; }
        [DataMember]
        public string PickupContactNo { get; set; }
        [DataMember]
        public string DropContactNo { get; set; }
        [DataMember]
        public string PickupStopDriverName { get; set; }
        [DataMember]
        public Boolean IsOneWay { get; set; }
        [DataMember]
        public string DateFrom { get; set; }
        [DataMember]
        public string DateTo { get; set; }
        [DataMember]
        public long ClassID { get; set; }
        [DataMember]
        public long SectionID { get; set; }
        [DataMember]
        public long LoginID { get; set; }
        [DataMember]
        public long StudentRouteStopMapIID { get; set; } 
        
        [DataMember]
        public byte? SchoolID { get; set; } 
        
        [DataMember]
        public string SchoolShortName { get; set; } 
        
        [DataMember]
        public string Class { get; set; } 
        
        [DataMember]
        public string Section { get; set; } 
    }
}
