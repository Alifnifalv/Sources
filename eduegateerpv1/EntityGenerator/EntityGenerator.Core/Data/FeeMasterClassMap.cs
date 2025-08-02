using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeMasterClassMaps", Schema = "schools")]
    public partial class FeeMasterClassMap
    {
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassFeeMasterID")]
        [InverseProperty("FeeMasterClassMaps")]
        public virtual ClassFeeMaster ClassFeeMaster { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("FeeMasterClassMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FeeMasterClassMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [InverseProperty("FeeMasterClassMap")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("FeeMasterClassMap")]
        public virtual ICollection<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }
    }
}
