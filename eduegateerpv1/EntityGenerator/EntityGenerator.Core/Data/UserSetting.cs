using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserSettings", Schema = "setting")]
    public partial class UserSetting
    {
        [Key]
        public long LoginID { get; set; }
        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string SettingCode { get; set; }
        [Key]
        public int CompanyID { get; set; }
        public int? SiteID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SettingValue { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Description { get; set; }
        public bool? ShowProductImageForPOS { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("UserSettings")]
        public virtual Company Company { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("UserSettings")]
        public virtual Login Login { get; set; }
    }
}
