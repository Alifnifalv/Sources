namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("UserScreenFieldSettings", Schema = "setting")]
    public partial class UserScreenFieldSetting
    {
        [Key]
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
