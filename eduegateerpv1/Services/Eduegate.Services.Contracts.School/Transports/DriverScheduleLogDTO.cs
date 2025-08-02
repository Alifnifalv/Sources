using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class DriverScheduleLogDTO : BaseMasterDTO
    {
        public DriverScheduleLogDTO()
        {
            StudentRouteStopMap = new StudentRouteStopMapDTO();
            StaffRouteStopMap = new StaffRouteStopMapDTO();
            Stops = new List<RouteStopFeeDTO>();
            InLog = new ScheduleLogInfoDTO();
            OutLog = new ScheduleLogInfoDTO();
        }

        [DataMember]
        public long DriverScheduleLogIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public DateTime? SheduleDate { get; set; }

        [DataMember]
        public int? RouteID { get; set; }

        [DataMember]
        public long? RouteStopMapID { get; set; }

        [DataMember]
        public long? VehicleID { get; set; }

        [DataMember]
        public int? SheduleLogStatusID { get; set; }

        [DataMember]
        public int? StopEntryStatusID { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ScheduleLogType { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string RouteCode { get; set; }

        [DataMember]
        public string StopName { get; set; }

        [DataMember]
        public string VehicleRegistrationNumber { get; set; }

        [DataMember]
        public ScheduleLogInfoDTO InLog { get; set; }

        [DataMember]
        public ScheduleLogInfoDTO OutLog { get; set; }

        #region Mobile app use
        [DataMember]
        public StudentRouteStopMapDTO StudentRouteStopMap { get; set; }

        [DataMember]
        public StaffRouteStopMapDTO StaffRouteStopMap { get; set; }

        [DataMember]
        public List<RouteStopFeeDTO> Stops { get; set; }

        [DataMember]
        public string VehicleType { get; set; }

        [DataMember]
        public bool? IsStudentIn { get; set; }

        [DataMember]
        public bool? IsStaffIn { get; set; }

        [DataMember]
        public bool? IsPickupStop { get; set; }

        [DataMember]
        public bool? IsDropStop { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public long? PassengerTypeID { get; set; }

        #endregion

        #region For MobileApp list view
        [DataMember]
        public string PassengerCode { get; set; }

        [DataMember]
        public string PassengerName { get; set; }

        [DataMember]
        public string ClassSection { get; set; }

        [DataMember]
        public string StatusMark { get; set; }
        #endregion
    }

    [DataContract]
    public class ScheduleLogInfoDTO
    {
        [DataMember]
        public long IID { get; set; }

        [DataMember]
        public int? StatusID { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ScheduleLogType { get; set; }

    }

}