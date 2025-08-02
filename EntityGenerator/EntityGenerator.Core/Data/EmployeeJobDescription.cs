using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeJobDescription", Schema = "payroll")]
    public partial class EmployeeJobDescription
    {
        public EmployeeJobDescription()
        {
            EmployeeJobDescriptionDetails = new HashSet<EmployeeJobDescriptionDetail>();
        }

        [Key]
        public long JobDescriptionIID { get; set; }
        public long? EmployeeID { get; set; }
        public long? ReportingToEmployeeID { get; set; }
        [StringLength(250)]
        public string JDReference { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JDDate { get; set; }
        [StringLength(250)]
        public string RevReference { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RevDate { get; set; }
        public string RoleSummary { get; set; }
        public string Undertaking { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsAgreementSigned { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AgreementSignedDate { get; set; }
        public long? SignatureContentID { get; set; }
        public long? JobApplicationID { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeJobDescriptionEmployees")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("JobApplicationID")]
        [InverseProperty("EmployeeJobDescriptions")]
        public virtual JobApplication JobApplication { get; set; }
        [ForeignKey("ReportingToEmployeeID")]
        [InverseProperty("EmployeeJobDescriptionReportingToEmployees")]
        public virtual Employee ReportingToEmployee { get; set; }
        [InverseProperty("JobDescription")]
        public virtual ICollection<EmployeeJobDescriptionDetail> EmployeeJobDescriptionDetails { get; set; }
    }
}
