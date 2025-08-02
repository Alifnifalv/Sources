using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("ReportPeriods", Schema = "schools")]
    public partial class ReportPeriod
    {
        public int PeriodID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PeriodName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PeriodRemarks { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PeriodCaption1 { get; set; }
        [MaxLength(50)]
        public byte[] Periodcaption2 { get; set; }
    }
}
