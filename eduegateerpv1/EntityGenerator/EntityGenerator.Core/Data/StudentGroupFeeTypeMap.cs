using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentGroupFeeTypeMaps", Schema = "schools")]
    public partial class StudentGroupFeeTypeMap
    {
        [Key]
        public long StudentGroupFeeTypeMapIID { get; set; }
        public long StudentGroupFeeMasterID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        public bool? IsPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PercentageAmount { get; set; }
        public string Formula { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("FeeMasterID")]
        [InverseProperty("StudentGroupFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("StudentGroupFeeTypeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("StudentGroupFeeMasterID")]
        [InverseProperty("StudentGroupFeeTypeMaps")]
        public virtual StudentGroupFeeMaster StudentGroupFeeMaster { get; set; }
    }
}
