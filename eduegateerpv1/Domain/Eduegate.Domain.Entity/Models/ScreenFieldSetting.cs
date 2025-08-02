namespace Eduegate.Domain.Entity.Models
{
    using Eduegate.Domain.Entity.Models.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ScreenFieldSettings", Schema = "setting")]
    public partial class ScreenFieldSetting
    {
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

        public virtual Sequence Sequence { get; set; }

        public virtual TextTransformType TextTransformType { get; set; }
    }
}
