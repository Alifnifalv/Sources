using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserScreenFieldSettings", Schema = "setting")]
    public partial class UserScreenFieldSetting
    {
        [Key]
        public long UserScreenFieldSettingID { get; set; }
        public long? LoginID { get; set; }
        public long? ScreenID { get; set; }
        public long? ScreenFieldID { get; set; }
        [StringLength(50)]
        public string DefaultValue { get; set; }
        [StringLength(50)]
        public string DefaultFormat { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("UserScreenFieldSettings")]
        public virtual Login Login { get; set; }
        [ForeignKey("ScreenID")]
        [InverseProperty("UserScreenFieldSettings")]
        public virtual ScreenMetadata Screen { get; set; }
        [ForeignKey("ScreenFieldID")]
        [InverseProperty("UserScreenFieldSettings")]
        public virtual ScreenField ScreenField { get; set; }
    }
}
