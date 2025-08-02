using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScreenShortCuts", Schema = "setting")]
    public partial class ScreenShortCut
    {
        [Key]
        public long ScreenShortCutID { get; set; }
        public long? ScreenID { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string KeyCode { get; set; }
        [StringLength(50)]
        public string ShortCutKey { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Action { get; set; }

        [ForeignKey("ScreenID")]
        [InverseProperty("ScreenShortCuts")]
        public virtual ScreenMetadata Screen { get; set; }
    }
}
