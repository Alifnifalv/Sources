using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class VehicleDetailsMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public VehicleDetailsMapDTO()
        {
            Vehicle = new KeyValueDTO();
        }
        [DataMember]
        public long VehicleDetailMapIID { get; set; }

        [DataMember]
        public long? VehicleID { get; set; }

        [DataMember]
        public KeyValueDTO Vehicle { get; set; }

        [DataMember]
        public DateTime? RegistrationDate { get; set; }

        [DataMember]
        public DateTime? RegistrationExpiryDate { get; set; }

        [DataMember]
        public DateTime? InsuranceIssueDate { get; set; }

        [DataMember]
        public DateTime? InsuranceExpiryDate { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}
