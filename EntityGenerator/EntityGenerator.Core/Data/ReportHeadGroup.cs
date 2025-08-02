using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ReportHeadGroups", Schema = "mutual")]
    public partial class ReportHeadGroup
    {
        public ReportHeadGroup()
        {
            SalaryComponents = new HashSet<SalaryComponent>();
        }

        [Key]
        public int ReportHeadGroupID { get; set; }
        [StringLength(100)]
        public string ReportHeadGroupName { get; set; }

        [InverseProperty("ReportHeadGroup")]
        public virtual ICollection<SalaryComponent> SalaryComponents { get; set; }
    }
}
