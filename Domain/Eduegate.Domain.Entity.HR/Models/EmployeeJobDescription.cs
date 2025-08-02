using Eduegate.Domain.Entity.HR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR
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
        public string JDReference { get; set; }
        public DateTime? JDDate { get; set; }
        public string RevReference { get; set; }
        public DateTime? RevDate { get; set; }
        public string RoleSummary { get; set; }
        public string Undertaking { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee ReportingToEmployee { get; set; }
        public virtual ICollection<EmployeeJobDescriptionDetail> EmployeeJobDescriptionDetails { get; set; }
    }
}
