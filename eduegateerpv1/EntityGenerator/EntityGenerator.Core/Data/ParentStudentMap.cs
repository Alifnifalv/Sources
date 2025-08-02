using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ParentStudentMaps", Schema = "schools")]
    public partial class ParentStudentMap
    {
        [Key]
        public long ParentStudentMapIID { get; set; }
        public long? ParentID { get; set; }
        public long? StudentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("StudentID")]
        [InverseProperty("ParentStudentMaps")]
        public virtual Student Student { get; set; }
    }
}
