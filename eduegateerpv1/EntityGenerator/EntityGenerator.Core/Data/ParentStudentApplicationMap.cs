using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ParentStudentApplicationMaps", Schema = "schools")]
    public partial class ParentStudentApplicationMap
    {
        [Key]
        public long ParentStudentApplicationMapIID { get; set; }
        public long? ParentID { get; set; }
        public long? ApplicationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ParentID")]
        [InverseProperty("ParentStudentApplicationMaps")]
        public virtual Parent Parent { get; set; }
    }
}
