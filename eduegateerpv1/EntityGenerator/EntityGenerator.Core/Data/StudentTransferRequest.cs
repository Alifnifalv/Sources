using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentTransferRequests", Schema = "schools")]
    [Index("SchoolID", Name = "IDX_StudentTransferRequests_SchoolID_StudentID")]
    [Index("SchoolID", Name = "IDX_StudentTransferRequests_SchoolID_StudentID__TransferRequestStatusID")]
    [Index("StudentID", Name = "IDX_StudentTransferRequests_StudentID_CreatedDate")]
    [Index("StudentID", "SchoolID", Name = "IDX_StudentTransferRequests_StudentID__SchoolID_")]
    [Index("StudentID", "SchoolID", Name = "IDX_StudentTransferRequests_StudentID__SchoolID_ExpectingRelivingDate__OtherReason__CreatedDate__Up")]
    [Index("TransferRequestStatusID", Name = "IDX_StudentTransferRequests_TransferRequestStatusID_StudentID")]
    [Index("TransferRequestStatusID", "SchoolID", Name = "IDX_StudentTransferRequests_TransferRequestStatusID__SchoolID_StudentID")]
    public partial class StudentTransferRequest
    {
        [Key]
        public long StudentTransferRequestIID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpectingRelivingDate { get; set; }
        public string OtherReason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte TransferRequestStatusID { get; set; }
        public bool? IsTransferRequested { get; set; }
        public byte? TransferRequestReasonID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsSchoolChange { get; set; }
        public bool? IsLeavingCountry { get; set; }
        public string SchoolRemarks { get; set; }
        public string Concern { get; set; }
        public string PositiveAspect { get; set; }
        public bool? IsMailSent { get; set; }
        [StringLength(500)]
        public string TCContentID { get; set; }
        public bool? IsChequeIssued { get; set; }
        public bool? IsTCCollected { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentTransferRequests")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentTransferRequests")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentTransferRequests")]
        public virtual Student Student { get; set; }
        [ForeignKey("TransferRequestReasonID")]
        [InverseProperty("StudentTransferRequests")]
        public virtual StudentTransferRequestReason TransferRequestReason { get; set; }
        [ForeignKey("TransferRequestStatusID")]
        [InverseProperty("StudentTransferRequests")]
        public virtual TransferRequestStatus TransferRequestStatus { get; set; }
    }
}
