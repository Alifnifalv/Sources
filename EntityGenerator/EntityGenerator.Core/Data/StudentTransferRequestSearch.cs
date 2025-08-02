using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentTransferRequestSearch
    {
        public long StudentTransferRequestIID { get; set; }
        [Unicode(false)]
        public string TCApplicationNo { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? StudentID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ExpectingRelivingDate { get; set; }
        public string OtherReason { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public byte TransferRequestStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(502)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AppliedDate { get; set; }
        [StringLength(20)]
        public string QID { get; set; }
        [StringLength(50)]
        public string Contact { get; set; }
        [StringLength(101)]
        public string ClassSection { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public string Concern { get; set; }
        public string PositiveAspect { get; set; }
        public string SchoolRemarks { get; set; }
    }
}
