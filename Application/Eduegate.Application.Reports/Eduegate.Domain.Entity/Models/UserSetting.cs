namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.UserSettings")]
    public partial class UserSetting
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LoginID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SettingCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        public int? SiteID { get; set; }

        [StringLength(100)]
        public string SettingValue { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public virtual Login Login { get; set; }

        public virtual Company Company { get; set; }
    }
}
