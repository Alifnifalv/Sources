using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ViewColumnCultureDatas", Schema = "setting")]
    public partial class ViewColumnCultureData
    {
        [Key]
        public long ViewColumnID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string ColumnName { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ViewColumnCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ViewColumnID")]
        [InverseProperty("ViewColumnCultureDatas")]
        public virtual ViewColumn ViewColumn { get; set; }
    }
}
