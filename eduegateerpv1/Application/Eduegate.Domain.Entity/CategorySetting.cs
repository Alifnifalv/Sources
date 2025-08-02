namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.CategorySettings")]
    public partial class CategorySetting
    {
        [Key]
        public long CategorySettingsID { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string SettingCode { get; set; }

        [StringLength(50)]
        public string SettingValue { get; set; }

        public byte? UIControlTypeID { get; set; }

        public int? LookUpID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public virtual Category Category { get; set; }

        public virtual UIControlType UIControlType { get; set; }
    }
}
