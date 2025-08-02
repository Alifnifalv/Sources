namespace Eduegate.Domain.Entity.School.Models
{
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeStructureFeeMaps", Schema = "schools")]
    public partial class FeeStructureFeeMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeStructureFeeMap()
        {
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeStructureMontlySplitMaps = new HashSet<FeeStructureMontlySplitMap>();
        }

        [Key]
        public long FeeStructureFeeMapIID { get; set; }

        public long FeeStructureID { get; set; }

        public int? FeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual FeeStructure FeeStructure { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeStructureMontlySplitMap> FeeStructureMontlySplitMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }


    }
}
