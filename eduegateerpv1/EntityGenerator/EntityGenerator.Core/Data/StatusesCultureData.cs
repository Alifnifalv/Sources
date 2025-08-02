using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StatusesCultureDatas", Schema = "mutual")]
    public partial class StatusesCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public byte StatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("StatusesCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("CultureID")]
        [InverseProperty("StatusesCultureDatas")]
        public virtual Status CultureNavigation { get; set; }
    }
}
