using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Report_Percentage_Params", Schema = "mutual")]
    public partial class Report_Percentage_Params
    {
        [StringLength(100)]
        [Unicode(false)]
        public string ReportName { get; set; }
        [StringLength(100)]
        public string LabelRange { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PercentageFrom { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PercentageTo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
