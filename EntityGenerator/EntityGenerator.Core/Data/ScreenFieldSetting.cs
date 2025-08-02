using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScreenFieldSettings", Schema = "setting")]
    public partial class ScreenFieldSetting
    {
        [Key]
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
        [StringLength(50)]
        public string ScreenFieldName { get; set; }

        [ForeignKey("ScreenID")]
        [InverseProperty("ScreenFieldSettings")]
        public virtual ScreenMetadata Screen { get; set; }
        [ForeignKey("ScreenFieldID")]
        [InverseProperty("ScreenFieldSettings")]
        public virtual ScreenField ScreenField { get; set; }
        [ForeignKey("SequenceID")]
        [InverseProperty("ScreenFieldSettings")]
        public virtual Sequence Sequence { get; set; }
        [ForeignKey("TextTransformTypeId")]
        [InverseProperty("ScreenFieldSettings")]
        public virtual TextTransformType TextTransformType { get; set; }
    }
}
