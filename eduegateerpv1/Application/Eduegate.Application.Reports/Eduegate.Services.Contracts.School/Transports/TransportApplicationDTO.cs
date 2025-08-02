using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class TransportApplicationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TransportApplicationDTO()
        {
            TransportApplicationStudentMaps = new List<TransportApplicationStudentMapDTO>();
            AcademicYear = new KeyValueDTO();

        }

        [DataMember]
        public long TransportApplicationIID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string ApplicationNumber { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        [StringLength(200)]
        public string LandMark { get; set; }

        [DataMember]
        [StringLength(50)]
        public string FatherName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string MotherName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string MotherContactNumber { get; set; }

        [DataMember]
        [StringLength(50)]
        public string MotherEmailID { get; set; }

        [DataMember]
        [StringLength(20)]
        public string Building_FlatNo { get; set; }

        [DataMember]
        [StringLength(50)]
        public string StreetNo { get; set; }

        [DataMember]
        [StringLength(50)]
        public string StreetName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LocationNo { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LocationName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string ZoneNo { get; set; }

        [DataMember]
        public short? ZoneID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string City { get; set; }

        [DataMember]
        [StringLength(50)]
        public string EmergencyContactNumber { get; set; }

        [DataMember]
        [StringLength(50)]
        public string EmergencyEmailID { get; set; }

        [DataMember]
        public DateTime? PickUpTime { get; set; }

        [DataMember]
        public DateTime? DropOffTime { get; set; }

        [DataMember]
        [StringLength(75)]
        public string PickUpStop { get; set; }

        [DataMember]
        [StringLength(75)]
        public string DropOffStop { get; set; }

        [DataMember]
        public short? StreetID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string FatherContactNumber { get; set; }

        [DataMember]
        [StringLength(50)]
        public string FatherEmailID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public long? PickupStopMapID { get; set; }

        [DataMember]
        public long? DropStopMapID { get; set; }

        [DataMember]
        public string BuildingNo_Drop { get; set; }

        [DataMember]
        public string StreetNo_Drop { get; set; }

        [DataMember]
        public string StreetName_Drop { get; set; }

        [DataMember]
        public string LocationNo_Drop { get; set; }

        [DataMember]
        public string LocationName_Drop { get; set; }

        [DataMember]
        public string ZoneNo_Drop { get; set; }

        [DataMember]
        public string LandMark_Drop { get; set; }

        [DataMember]
        public bool? IsRouteDifferent { get; set; }
        [DataMember]
        public bool? IsNewStops { get; set; }

        [DataMember]
        public bool? IsMedicalCondition { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public string DateFrom { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public List<TransportApplicationStudentMapDTO> TransportApplicationStudentMaps { get; set; }

        [DataMember]
        public long? TransportApplctnStudentMapID { get; set; }
    }
}