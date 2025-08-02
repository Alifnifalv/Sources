using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobDescription", Schema = "payroll")]
    public partial class JobDescription
    {
        public JobDescription()
        {
            JobDescriptionDetails = new HashSet<JobDescriptionDetail>();
        }

        [Key]
        public long JDMasterIID { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
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
        public long? DepartmentID { get; set; }
        public int? DesignationID { get; set; }

        [ForeignKey("DepartmentID")]
        [InverseProperty("JobDescriptions")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("DesignationID")]
        [InverseProperty("JobDescriptions")]
        public virtual Designation Designation { get; set; }
        [ForeignKey("ReportingToEmployeeID")]
        [InverseProperty("JobDescriptions")]
        public virtual Employee ReportingToEmployee { get; set; }
        [InverseProperty("JDMaster")]
        public virtual ICollection<JobDescriptionDetail> JobDescriptionDetails { get; set; }
    }
}
