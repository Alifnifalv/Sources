using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FilterColumnCultureDatas", Schema = "setting")]
    public partial class FilterColumnCultureData
    {
        [Key]
        public long FilterColumnID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string ColumnCaption { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("FilterColumnCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("FilterColumnID")]
        [InverseProperty("FilterColumnCultureDatas")]
        public virtual FilterColumn FilterColumn { get; set; }
    }
}
