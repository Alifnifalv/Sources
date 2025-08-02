using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TextTransformTypes", Schema = "setting")]
    public partial class TextTransformType
    {
        public TextTransformType()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
        }

        [Key]
        public byte TextTransformTypeId { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("TextTransformType")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
    }
}
