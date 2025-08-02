using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class StudentRouteStopMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentRouteStopMapDTO()
        {
            Student = new KeyValueDTO();
            RouteType = new KeyValueDTO();
            RouteStopMap = new KeyValueDTO();
            PickupStopMap = new KeyValueDTO();
            DropStopMap = new KeyValueDTO();
            PickupSeatMap = new SeatingAvailabilityDTO();
            DropSeatMap = new SeatingAvailabilityDTO();
            TransporStatus = new KeyValueDTO();
            PickUpRoute = new KeyValueDTO();
            DropRoute = new KeyValueDTO();
            AcademicYear = new KeyValueDTO();
            FeePeriod = new List<KeyValueDTO>();
            MonthlySplitDTO = new List<StudentRouteMonthlySplitDTO>();
        }

        [DataMember]
        public long StudentRouteStopMapIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public byte? RouteTypeID { get; set; }

        [DataMember]
        public KeyValueDTO RouteType { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public long? RouteStopMapID { get; set; }

        [DataMember]
        public KeyValueDTO RouteStopMap { get; set; }

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
        public bool? Approve { get; set; }

        [DataMember]
        public bool? Termsandco { get; set; }

        [DataMember]
        public string ApplicationNumber { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public SeatingAvailabilityDTO DropSeatMap { get; set; }

        [DataMember]
        public SeatingAvailabilityDTO PickupSeatMap { get; set; }

        [DataMember]
        public string DropRouteCode { get; set; }

        [DataMember]
        public string PickUpRouteCode { get; set; }

        [DataMember]
        public string RouteTypeName { get; set; }

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
        public string RoutePickName { get; set; }

        [DataMember]
        public string RouteDropName { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public DateTime? CancelDate { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }

        #region Mobile app use
        [DataMember]
        public bool? IsPickupStop { get; set; }

        [DataMember]
        public bool? IsDropStop { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string StudentProfile { get; set; }

        [DataMember]
        public bool? IsStudentIn { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public long? DriverScheduleLogIID { get; set; }

        [DataMember]
        public string StopName { get; set; }

        [DataMember]
        public string ScheduleLogType { get; set; }

        [DataMember]
        public string Status { get; set; }
        #endregion

        [DataMember]
        public List<KeyValueDTO> FeePeriod { get; set; }

        [DataMember]
        public long? TransportApplctnStudentMapID { get; set; }

        [DataMember]
        public List<StudentRouteMonthlySplitDTO> MonthlySplitDTO { get; set; }

        #region for email notification reference
        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public string OldDropRoute { get; set; }

        [DataMember]
        public string OldPickUpRoute { get; set; }

        [DataMember]
        public string OldDropStopMap { get; set; }

        [DataMember]
        public string OldPickupStopMap { get; set; }

        [DataMember]
        public long? OldPickupStopMapID { get; set; }

        [DataMember]
        public long? OldDropStopMapID { get; set; }

        [DataMember]
        public int? OldPickupRouteID { get; set; }

        [DataMember]
        public int? OldDropStopRouteID { get; set; }
        #endregion
    }
}