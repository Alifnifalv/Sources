using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AuditTypes", Schema = "account")]
    public partial class AuditType
    {
        public AuditType()
        {
            FiscalYears = new HashSet<FiscalYear>();
        }

        [Key]
        public int AuditTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("AuditTypeNavigation")]
        public virtual ICollection<FiscalYear> FiscalYears { get; set; }
    }
}
