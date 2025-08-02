using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategorySettings", Schema = "catalog")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("CategorySettings")]
        public virtual Category Category { get; set; }
        [ForeignKey("UIControlTypeID")]
        [InverseProperty("CategorySettings")]
        public virtual UIControlType UIControlType { get; set; }
    }
}
