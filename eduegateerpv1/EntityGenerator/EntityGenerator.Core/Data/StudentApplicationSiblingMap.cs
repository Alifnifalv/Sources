using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentApplicationSiblingMaps", Schema = "schools")]
    public partial class StudentApplicationSiblingMap
    {
        [Key]
        public long StudentApplicationSiblingMapIID { get; set; }
        public long SiblingID { get; set; }
        public long? ApplicationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ApplicationID")]
        [InverseProperty("StudentApplicationSiblingMaps")]
        public virtual StudentApplication Application { get; set; }
        [ForeignKey("SiblingID")]
        [InverseProperty("StudentApplicationSiblingMaps")]
        public virtual Student Sibling { get; set; }
    }
}
