using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BranchCultureDatas", Schema = "mutual")]
    public partial class BranchCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long BranchID { get; set; }
        [StringLength(255)]
        public string BranchName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("BranchCultureDatas")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CultureID")]
        [InverseProperty("BranchCultureDatas")]
        public virtual Culture Culture { get; set; }
    }
}
