using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class DriverVehicleTrackingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public DriverVehicleTrackingDTO()
        {
      
            VehicleAttendantDetails = new List<VehicleAttendantDTO>();

        }

        [DataMember]

        public decimal RouteStartKM { get; set; }
        [DataMember]

        public int EmployeeID { get; set; }
        [DataMember]

        public DateTime? StartTime { get; set; }
        [DataMember]

        public int VehicleID { get; set; }
        [DataMember]

        public int RouteID { get; set; }
        [DataMember]

        public decimal VehicleTrackingIID { get; set; }
        [DataMember]

        public DateTime? EndTime { get; set; }
        [DataMember]

        public decimal RouteEndKM { get; set; }
        [DataMember]

        public string AttachmentID1 { get; set; }
        [DataMember]

        public string AttachmentID2 { get; set; }

     

        #region Mobile app use
        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public List<VehicleAttendantDTO> VehicleAttendantDetails { get; set; }
        #endregion
    }
}