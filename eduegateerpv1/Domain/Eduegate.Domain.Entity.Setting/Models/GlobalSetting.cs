namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GlobalSettings", Schema = "setting")]
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
