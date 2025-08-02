using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ViewCultureDatas", Schema = "setting")]
    public partial class ViewCultureData
    {
        [Key]
        public long ViewID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        public string ViewDescription { get; set; }
        public string ViewTitle { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ViewCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ViewID")]
        [InverseProperty("ViewCultureDatas")]
        public virtual View View { get; set; }
    }
}
