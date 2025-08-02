using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{

    [DataContract]
    public class RouteShiftingStaffMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RouteShiftingStaffMapDTO()
        {
            Staff = new KeyValueDTO();
            RouteStopMap = new KeyValueDTO();
        }

        [DataMember]
        public long? StaffRouteStopMapID { get; set; }

        [DataMember]
        public long StaffRouteStopMapLogIID { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public long? RouteStopMapID { get; set; }

        [DataMember]
        public long? PickupStopMapID { get; set; }

        [DataMember]
        public long? DropStopMapID { get; set; }

        [DataMember]
        public bool? IsOneWay { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public int? PickupRouteID { get; set; }

        [DataMember]
        public int? DropStopRouteID { get; set; }

        [DataMember]
        public bool? Termsandco { get; set; }

        [DataMember]
        public string StudentRouteStopCode { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public long? TransportStatusID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public DateTime? PickupTime { get; set; }

        [DataMember]
        public DateTime? DropTime { get; set; }

        [DataMember]
        public int? IsRouteShifted { get; set; }

        [DataMember]
        public KeyValueDTO RouteType { get; set; }

        [DataMember]
        public KeyValueDTO Staff { get; set; }

        [DataMember]
        public KeyValueDTO RouteStopMap { get; set; }

        [DataMember]
        public string PickupStopMap { get; set; }

        [DataMember]
        public string DropStopMap { get; set; }


        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }


        [DataMember]
        public string FromSchool { get; set; }

        [DataMember]
        public byte? FromSchoolID { get; set; }

        [DataMember]
        public string ToSchool { get; set; }

        [DataMember]
        public byte? ToSchoolID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string OldPickUpStop { get; set; }

        [DataMember]
        public string OldDropStop { get; set; }

        [DataMember]
        public string OldPickUpStopEdit { get; set; }

        [DataMember]
        public string OldDropStopEdit { get; set; }

        [DataMember]
        public string DateFromString { get; set; }

        [DataMember]
        public string DateToString { get; set; }
    }
}