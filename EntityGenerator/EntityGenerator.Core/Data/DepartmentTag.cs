using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DepartmentTags", Schema = "hr")]
    public partial class DepartmentTag
    {
        [Key]
        public long DepartmentTagIID { get; set; }
        public int? DepartmentID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("DepartmentID")]
        [InverseProperty("DepartmentTags")]
        public virtual Department Department { get; set; }
    }
}
