using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class JobDescriptionReview
    {
        public long ApplicationIID { get; set; }
        public long InterviewID { get; set; }
        public long? JobID { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        public long? ApplicantID { get; set; }
        [Required]
        public string ApplicantName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AgreementSignedDate { get; set; }
        public bool? IsAgreementSigned { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsSigned { get; set; }
        [Required]
        [StringLength(554)]
        public string ReportingToEmployee { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
