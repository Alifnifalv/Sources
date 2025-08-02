using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassAssociateTeacherMaps", Schema = "schools")]
    public partial class ClassAssociateTeacherMap
    {
        [Key]
        public long ClassAssociateTeacherMapIID { get; set; }
        public long? TeacherID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? ClassClassTeacherMapID { get; set; }

        [ForeignKey("ClassClassTeacherMapID")]
        [InverseProperty("ClassAssociateTeacherMaps")]
        public virtual ClassClassTeacherMap ClassClassTeacherMap { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("ClassAssociateTeacherMaps")]
        public virtual Employee Teacher { get; set; }
    }
}
