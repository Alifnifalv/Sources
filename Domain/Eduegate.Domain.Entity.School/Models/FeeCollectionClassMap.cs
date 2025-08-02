namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeCollectionClassMaps", Schema = "schools")]
    public partial class FeeCollectionClassMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeCollectionClassMap()
        {
            FeeCollectionMonthlySplits = new HashSet<FeeCollectionMonthlySplit>();
        }

        [Key]
        public long FeeCollectionClassMapsIID { get; set; }

        public long? FEECollectionID { get; set; }

        public int? FeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual FeeCollection FeeCollection { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }
    }
}
