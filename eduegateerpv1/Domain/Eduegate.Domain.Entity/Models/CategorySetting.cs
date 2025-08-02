using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CategorySettings", Schema = "catalog")]
    public partial class CategorySetting
    {
        [Key]
        public long CategorySettingsID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string SettingCode { get; set; }
        public string SettingValue { get; set; }
        public Nullable<byte> UIControlTypeID { get; set; }
        public Nullable<int> LookUpID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public virtual Category Category { get; set; }
        public virtual UIControlType UIControlType { get; set; }
        public string Description { get; set; }
    }
}
