using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AreaCultureDatas", Schema = "mutual")]
    public partial class AreaCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public int AreaID { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }

        [ForeignKey("AreaID")]
        [InverseProperty("AreaCultureDatas")]
        public virtual Area Area { get; set; }
        [ForeignKey("CultureID")]
        [InverseProperty("AreaCultureDatas")]
        public virtual Culture Culture { get; set; }
    }
}
