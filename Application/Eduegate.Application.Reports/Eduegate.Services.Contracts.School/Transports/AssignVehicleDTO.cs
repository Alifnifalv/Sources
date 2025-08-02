using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class AssignVehicleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AssignVehicleDTO()
        {
            Employee = new KeyValueDTO();
            Vehicle = new KeyValueDTO();
            Attendanter = new List<KeyValueDTO>();
            Routes = new KeyValueDTO();
            VehicleAttendantDetails = new List<VehicleAttendantDTO>();
        }

        [DataMember]
        public long AssignVehicleMapIID { get; set; }

        [DataMember]
        public System.DateTime? DateFrom { get; set; }

        [DataMember]
        public System.DateTime? DateTo { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public long? VehicleID { get; set; }

        [DataMember]
        public KeyValueDTO Vehicle { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public List<KeyValueDTO> Attendanter { get; set; }

        [DataMember]
        public string DriverName { get; set; }

        [DataMember]
        public string AttenderName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? RouteID { get; set; }

        [DataMember]
        public KeyValueDTO Routes { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }

        #region Mobile app use
        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public List<VehicleAttendantDTO> VehicleAttendantDetails { get; set; }
        #endregion
    }
}