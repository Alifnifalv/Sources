using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentTransferRequestDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long StudentTransferRequestIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public DateTime? ExpectingRelivingDate { get; set; }

        [DataMember]
        public string OtherReason { get; set; }

        [DataMember]
        public byte? TransferRequestStatusID { get; set; }

        [DataMember]
        public string TransferRequestStatusDescription { get; set; }

        [DataMember]
        public string ExpectingRelivingDateString { get; set; }

        [DataMember]
        public bool? IsTransferRequested { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public string CreatedDateString { get; set; }

        [DataMember]
        public byte? TransferRequestReasonID { get; set; }

        [DataMember]
        public string StudentTransferRequestReasons { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public string TCAppNumber { get; set; }

        [DataMember]
        public bool? IsSchoolChange { get; set; }

        [DataMember]
        public bool? IsLeavingCountry { get; set; }

        [DataMember]
        public string Concern { get; set; }

        [DataMember]
        public string SchoolRemarks { get; set; }

        [DataMember]
        public string PositiveAspect { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public bool? IsMailSent { get; set; }

        [DataMember]
        public string ContentFileIID { get; set; }

        [DataMember]
        public bool? IsChequeIssued { get; set; }

        [DataMember]
        public bool? IsTCCollected { get; set; }

        #region For TC Generation

        [DataMember]
        public string OtherRemarks { get; set; }

        [DataMember]
        public DateTime? IssuedDate { get; set; }

        [DataMember]
        public string CBSC { get; set; }

        [DataMember]
        public string PassedorFailedString { get; set; }

        [DataMember]
        public bool? isWithLog { get; set; }

        #endregion
    }
}