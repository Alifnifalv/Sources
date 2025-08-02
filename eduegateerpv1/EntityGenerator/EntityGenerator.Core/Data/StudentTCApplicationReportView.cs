using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentTCApplicationReportView
    {
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(33)]
        [Unicode(false)]
        public string TCApplicationNo { get; set; }
        public long StudentTransferRequestIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? StudentID { get; set; }
        [Required]
        [StringLength(501)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpectingRelivingDate { get; set; }
        public string OtherReason { get; set; }
        public byte? TransferRequestReasonID { get; set; }
        public byte TransferRequestStatusID { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsSchoolChange { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsLeavingCountry { get; set; }
        public string SchoolRemarks { get; set; }
        public string Concern { get; set; }
        public string PositiveAspect { get; set; }
        [StringLength(50)]
        public string TransferStatus { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        [StringLength(50)]
        public string GuardianPhone { get; set; }
    }
}
