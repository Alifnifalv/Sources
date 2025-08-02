using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScreenMetadataCultureDatas", Schema = "setting")]
    public partial class ScreenMetadataCultureData
    {
        [Key]
        public long ScreenID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        public string ListButtonDisplayName { get; set; }
        public string DisplayName { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ScreenMetadataCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ScreenID")]
        [InverseProperty("ScreenMetadataCultureDatas")]
        public virtual ScreenMetadata Screen { get; set; }
    }
}
