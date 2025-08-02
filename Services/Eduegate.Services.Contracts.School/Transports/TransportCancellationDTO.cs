using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class TransportCancellationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TransportCancellationDTO()
        {

        }

        [DataMember]
        public long RequestIID { get; set; }
        [DataMember]
        public long? StudentRouteStopMapID { get; set; }
        [DataMember]
        public DateTime? AppliedDate { get; set; }
        [DataMember]
        public DateTime? ExpectedCancelDate { get; set; }
        [DataMember]
        public DateTime? ApprovedDate { get; set; }
        [DataMember]
        public int? ApprovedBy { get; set; }
        [DataMember]
        public int? StatusID { get; set; }  

        [DataMember]
        public int? PreviousStatusID { get; set; }

        [DataMember]
        public bool? CancelRequest { get; set; }

        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string RemarksBySchool { get; set; }
        [DataMember]
        public bool? CheckBoxForSiblings { get; set; }
        [DataMember]
        public bool? CheckBoxForDeclaration { get; set; }
        [DataMember]
        public long? StudentRouteStopMapIID { get; set; }

        [DataMember]
        public string RequestStatus { get; set; }

        [DataMember]
        public string AppliedDateString { get; set; }   
        
        [DataMember]
        public string ExpectedCancelDateString { get; set; }

        [DataMember]
        public long? StudentID { get; set; }
        
        [DataMember]
        public string StudentName { get; set; }  
        
        [DataMember]
        public string AdmissionNumber { get; set; }   
        
        [DataMember]
        public string ClassSection { get; set; }

        [DataMember]
        public bool? IsOneWay { get; set; } 
        
        [DataMember]
        public string PickupStopMapName { get; set; } 
        
        [DataMember]
        public string DropStopMapName { get; set; } 
        
        [DataMember]
        public string PickupRouteCode { get; set; }   
        
        [DataMember]
        public string DropStopRouteCode { get; set; }   
        
        [DataMember]
        public string FeeDues { get; set; }

    }
}