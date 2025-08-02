namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeStructureMontlySplitMaps")]
    public partial class FeeStructureMontlySplitMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeStructureMontlySplitMap()
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
