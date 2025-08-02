namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeStructureMontlySplitMaps", Schema = "schools")]
    public partial class FeeStructureMontlySplitMap
    {
        public  FeeStructureMontlySplitMap()
        { 
        FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
        }
        [Key]
        public long FeeStructureMontlySplitMapIID { get; set; }

        public long? FeeStructureFeeMapID { get; set; }

        public byte? MonthID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public int? Year { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }

        public virtual FeeStructureFeeMap FeeStructureFeeMap { get; set; }
    }
}
