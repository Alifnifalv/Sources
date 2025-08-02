using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class StudentPickupRequestDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentPickupRequestDTO()
        {
            Student = new KeyValueDTO();

            StudentsList = new List<StudentPickupRequestDTO>();
            StudentPickersList = new List<StudentPickupRequestDTO>();
        }

        [DataMember]
        public long StudentPickupRequestIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public DateTime? RequestDate { get; set; }

        [DataMember]
        public byte? RequestStatusID { get; set; }

        [DataMember]
        public byte? PickedByID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string AdditionalInfo { get; set; }

        [DataMember]
        public string RequestCode { get; set; }

        [DataMember]
        public byte[] RequestCodeImage { get; set; }

        [DataMember]
        public long? PhotoContentID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string RequestStatus { get; set; }

        [DataMember]
        public string RequestStringDate { get; set; }

        [DataMember]
        public DateTime? PickedDate { get; set; }

        [DataMember]
        public TimeSpan? FromTime { get; set; }

        [DataMember]
        public TimeSpan? ToTime { get; set; }

        [DataMember]
        public string PickStringDate { get; set; }

        [DataMember]
        public DateTime? PickUpStringDate { get; set; }

        [DataMember]
        public string InspectionColor { get; set; }

        [DataMember]
        public string FromStringTime { get; set; }

        [DataMember]
        public string ToStringTime { get; set; }

        [DataMember]
        public string PickedByDescription { get; set; }

        //Student Pickup Daily request
        [DataMember]
        public long StudentPickerStudentMapIID { get; set; }

        [DataMember]
        public long StudentPickerIID { get; set; }

        [DataMember]
        public long StudentPickLogIID { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public DateTime? PickDate { get; set; }

        [DataMember]
        public string CreatedDateString { get; set; }

        [DataMember]
        public DateTime? PickDateEndTime { get; set; }

        [DataMember]
        public string PickDateString { get; set; }


        [DataMember]
        public string PickUpBy{ get; set; }

        [DataMember]
        public string PickByFullName { get; set; }

        [DataMember]
        public string QRCODE { get; set; }

        [DataMember]
        public string ClassSection { get; set; }

        [DataMember]
        public bool IsActive {get; set;}


        [DataMember]
        public string GuardianName { get; set; }

        [DataMember]
        public string GuardianContact { get; set; }

        [DataMember]
        public string GuardianEmailID { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string StudentProfile { get; set; }

        [DataMember]
        public string PickerProfile { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public long? StudentPickerContentID { get; set; }

        [DataMember]
        public bool? LogStatus { get; set; }

        [DataMember]
        public string VisitorCode { get; set; }

        [DataMember]
        public long? PickUpLoginID { get; set; }

        //forListSetup
        [DataMember]
        public List<StudentPickupRequestDTO> StudentsList { get; set; }

        [DataMember]
        public List<StudentPickupRequestDTO> StudentPickersList { get; set; }
    }
}