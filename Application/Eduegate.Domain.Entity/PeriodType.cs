namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.PeriodTypes")]
    public partial class PeriodType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeriodTypeID { get; set; }

        [Column("PeriodType")]
        public int? PeriodType1 { get; set; }

        [StringLength(100)]
        public string PeriodTypeName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
