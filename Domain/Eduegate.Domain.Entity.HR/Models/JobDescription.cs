using Eduegate.Domain.Entity.HR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR
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
        public string Title { get; set; }
        public long? ReportingToEmployeeID { get; set; }
        public string JDReference { get; set; }
        public DateTime? JDDate { get; set; }
        public string RevReference { get; set; }
        public DateTime? RevDate { get; set; }
        public string RoleSummary { get; set; }
        public string Undertaking { get; set; }
        public string Responsibilities { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DepartmentID { get; set; }
        public int? DesignationID { get; set; }
        public virtual Departments1 Department { get; set; }
        public virtual Designation Designation { get; set; }

        public virtual Employee ReportingToEmployee { get; set; }
        public virtual ICollection<JobDescriptionDetail> JobDescriptionDetails { get; set; }
    }
}
