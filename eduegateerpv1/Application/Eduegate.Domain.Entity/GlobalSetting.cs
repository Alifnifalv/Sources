namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.GlobalSettings")]
    public partial class GlobalSetting
    {
        [Key]
        public int GlobalSettingIID { get; set; }

        [StringLength(300)]
        public string GlobalSettingKey { get; set; }

        [StringLength(300)]
        public string GlobalSettingValue { get; set; }
    }
}
