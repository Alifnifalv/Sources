namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentGroupFeeTypeMaps", Schema = "schools")]
    public partial class StudentGroupFeeTypeMap
    {
        [Key]
        public long StudentGroupFeeTypeMapIID { get; set; }

        public long StudentGroupFeeMasterID { get; set; }

        public int? FeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public bool? IsPercentage { get; set; }

        public decimal? PercentageAmount { get; set; }

        public string Formula { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual StudentGroupFeeMaster StudentGroupFeeMaster { get; set; }

    }
}
