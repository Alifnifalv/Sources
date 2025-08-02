using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentSiblingMaps", Schema = "schools")]
    [Index("StudentID", Name = "IDX_StudentSiblingMaps_StudentID_SiblingID")]
    public partial class StudentSiblingMap
    {
        [Key]
        public long StudentSiblingMapIID { get; set; }
        public long StudentID { get; set; }
        public long? SiblingID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? ParentID { get; set; }

        [ForeignKey("ParentID")]
        [InverseProperty("StudentSiblingMaps")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("SiblingID")]
        [InverseProperty("StudentSiblingMapSiblings")]
        public virtual Student Sibling { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentSiblingMapStudents")]
        public virtual Student Student { get; set; }
    }
}
