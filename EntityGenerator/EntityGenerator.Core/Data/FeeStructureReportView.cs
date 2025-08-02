using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeStructureReportView
    {
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string Expr1 { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Expr2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodTo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Expr3 { get; set; }
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }
    }
}
