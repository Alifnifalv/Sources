namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.FilterColumnUserValues")]
    public partial class FilterColumnUserValue
    {
        [Key]
        public long FilterColumnUserValueIID { get; set; }

        public long? ViewID { get; set; }

        public long? LoginID { get; set; }

        public long? FilterColumnID { get; set; }

        public byte? ConditionID { get; set; }

        [StringLength(500)]
        public string Value1 { get; set; }

        [StringLength(500)]
        public string Value2 { get; set; }

        [StringLength(500)]
        public string Value3 { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public virtual Login Login { get; set; }

        public virtual Condition Condition { get; set; }

        public virtual FilterColumn FilterColumn { get; set; }

        public virtual View View { get; set; }
    }
}
