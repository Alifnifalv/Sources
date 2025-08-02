namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.FiscalYear")]
    public partial class FiscalYear
    {
        [Key]
        public int FiscalYear_ID { get; set; }

        [StringLength(50)]
        public string FiscalYear_Name { get; set; }

        public int? FiscalYear_Status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EndDate { get; set; }

        [StringLength(10)]
        public string CurrentYear { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsHide { get; set; }

        public int? AuditType { get; set; }

        public DateTime? AuditedDate { get; set; }
    }
}
