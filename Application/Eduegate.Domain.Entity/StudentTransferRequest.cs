namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentTransferRequests")]
    public partial class StudentTransferRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StudentTransferRequestIID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? ExpectingRelivingDate { get; set; }

        public string OtherReason { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
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

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual Student Student { get; set; }

        public virtual StudentTransferRequestReason StudentTransferRequestReason { get; set; }

        public virtual TransferRequestStatus TransferRequestStatus { get; set; }
    }
}
