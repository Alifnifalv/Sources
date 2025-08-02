namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ScreenFieldSettings", Schema = "setting")]
    public partial class ScreenFieldSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenFieldSettingID { get; set; }

        public long? ScreenID { get; set; }

        public long? ScreenFieldID { get; set; }

        [StringLength(50)]
        public string DefaultValue { get; set; }

        [StringLength(50)]
        public string DefaultFormat { get; set; }

        [StringLength(50)]
        public string Prefix { get; set; }

        public byte? TextTransformTypeId { get; set; }

        public int? SequenceID { get; set; }

        public virtual ScreenField ScreenField { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }

        public virtual Sequence Squence { get; set; }

        public virtual TextTransformType TextTransformType { get; set; }
    }
}
