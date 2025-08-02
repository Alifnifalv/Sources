namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ReportPeriods")]
    public partial class ReportPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeriodID { get; set; }

        [StringLength(50)]
        public string PeriodName { get; set; }

        [StringLength(50)]
        public string PeriodRemarks { get; set; }

        [StringLength(50)]
        public string PeriodCaption1 { get; set; }

        [MaxLength(50)]
        public byte[] Periodcaption2 { get; set; }
    }
}
