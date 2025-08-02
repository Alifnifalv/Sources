using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentLeaveApplicationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long StudentLeaveApplicationIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public long? StudentID { get; set; }
        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public byte? FromSessionID { get; set; }

        [DataMember]
        public byte? ToSessionID { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Reason { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public byte? LeaveStatusID { get; set; }

        [DataMember]
        public string LeaveStatusDescription { get; set; }

        [DataMember]
        public string FromDateString { get; set; }

        [DataMember]
        public string ToDateString { get; set; }

        [DataMember]
        public string CreatedDateString { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string LeaveAppNumber { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool? IsLeaveWithoutPay { get; set; }

        [DataMember]
        public string StudentProfile { get; set; }

        #region Mobile app use
        [DataMember]
        public int? ApplicationSubmittedCount { get; set; }

        [DataMember]
        public int? ApplicationApprovedCount { get; set; }

        [DataMember]
        public int? ApplicationRejectedCount { get; set; }
        #endregion
    }
}