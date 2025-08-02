using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class StaffRouteStopMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StaffRouteStopMapDTO()
        {
            Staff = new KeyValueDTO();
            RouteType = new KeyValueDTO();
            PickupStopMap = new KeyValueDTO();
            DropStopMap = new KeyValueDTO();
            Academicyear = new KeyValueDTO();
            PickupSeatMap = new SeatingAvailabilityDTO();
            DropSeatMap = new SeatingAvailabilityDTO();
        }

        [DataMember]
        public long StaffRouteStopMapIID { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public KeyValueDTO Staff { get; set; }

        [DataMember]
        public long? PickupStopMapID { get; set; }

        [DataMember]
        public KeyValueDTO PickupStopMap { get; set; }

        [DataMember]
        public long? DropStopMapID { get; set; }

        [DataMember]
        public KeyValueDTO DropStopMap { get; set; }

        [DataMember]
        public bool? IsOneWay { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public SeatingAvailabilityDTO DropSeatMap { get; set; }

        [DataMember]
        public SeatingAvailabilityDTO PickupSeatMap { get; set; }

        [DataMember]
        public string DropRouteCode { get; set; }

        [DataMember]
        public string PickUpRouteCode { get; set; }

        [DataMember]
        public byte? RouteTypeID { get; set; }

        [DataMember]
        public bool? TermsAndConditions { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string RouteTypeName { get; set; }

        [DataMember]
        public KeyValueDTO RouteType { get; set; }

        [DataMember]
        public long? TransporStatusID { get; set; }

        [DataMember]
        public KeyValueDTO TransporStatus { get; set; }

        [DataMember]
        public int? PickupRouteID { get; set; }

        [DataMember]
        public KeyValueDTO PickUpRoute { get; set; }

        [DataMember]
        public int? DropStopRouteID { get; set; }

        [DataMember]
        public KeyValueDTO DropRoute { get; set; }

        [DataMember]
        public KeyValueDTO Academicyear { get; set; }

        [DataMember]
        public bool? Approve { get; set; }

        [DataMember]
        public DateTime? CancelDate { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        #region Mobile app use
        [DataMember]
        public bool? IsPickupStop { get; set; }

        [DataMember]
        public bool? IsDropStop { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string StaffProfile { get; set; }

        [DataMember]
        public bool? IsStaffIn { get; set; }

        [DataMember]
        public long? DriverScheduleLogIID { get; set; }

        [DataMember]
        public string StopName { get; set; }

        [DataMember]
        public string ScheduleLogType { get; set; }

        [DataMember]
        public string Status { get; set; }
        #endregion
    }
}