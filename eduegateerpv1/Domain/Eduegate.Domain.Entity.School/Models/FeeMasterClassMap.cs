namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeMasterClassMaps", Schema = "schools")]
    public partial class FeeMasterClassMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeMasterClassMap()
        {
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeMasterClassMontlySplitMaps = new HashSet<FeeMasterClassMontlySplitMap>();
        }

        [Key]
        public long FeeMasterClassMapIID { get; set; }

        public long? ClassFeeMasterID { get; set; }

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

        public virtual ClassFeeMaster ClassFeeMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }
    }
}
