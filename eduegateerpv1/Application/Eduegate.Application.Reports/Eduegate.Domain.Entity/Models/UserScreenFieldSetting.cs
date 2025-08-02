namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.UserScreenFieldSettings")]
    public partial class UserScreenFieldSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserScreenFieldSettingID { get; set; }

        public long? LoginID { get; set; }

        public long? ScreenID { get; set; }

        public long? ScreenFieldID { get; set; }

        [StringLength(50)]
        public string DefaultValue { get; set; }

        [StringLength(50)]
        public string DefaultFormat { get; set; }

        public virtual Login Login { get; set; }

        public virtual ScreenField ScreenField { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
