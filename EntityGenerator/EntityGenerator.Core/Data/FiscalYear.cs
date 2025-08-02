using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FiscalYear", Schema = "account")]
    public partial class FiscalYear
    {
        public FiscalYear()
        {
            Budget1 = new HashSet<Budget1>();
        }

        [Key]
        public int FiscalYear_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FiscalYear_Name { get; set; }
        public int? FiscalYear_Status { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? EndDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string CurrentYear { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsHide { get; set; }
        public int? AuditType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AuditedDate { get; set; }

        [ForeignKey("AuditType")]
        [InverseProperty("FiscalYears")]
        public virtual AuditType AuditTypeNavigation { get; set; }
        [InverseProperty("FinancialYear")]
        public virtual ICollection<Budget1> Budget1 { get; set; }
    }
}
