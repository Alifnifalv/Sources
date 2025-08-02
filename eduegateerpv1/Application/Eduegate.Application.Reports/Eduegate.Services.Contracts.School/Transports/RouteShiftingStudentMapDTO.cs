using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{

    [DataContract]
    public class RouteShiftingStudentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RouteShiftingStudentMapDTO()
        {
            Student = new KeyValueDTO();
            RouteStopMap = new KeyValueDTO();
        }

        [DataMember]
        public long StudentRouteStopMapLogIID { get; set; }

        [DataMember]
        public long? StudentRouteStopMapID { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

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
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public DateTime? PickupTime { get; set; }

        [DataMember]
        public DateTime? DropTime { get; set; }

        [DataMember]
        public int? IsRouteShifted { get; set; }

        [DataMember]
        public KeyValueDTO RouteType { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

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
        public string StudentName { get; set; }

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