using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentClassHistoryMaps", Schema = "schools")]
    public partial class StudentClassHistoryMap
    {
        [Key]
        public long StudentClassHistoryMapIID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChangeDate { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("StudentClassHistoryMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentClassHistoryMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentClassHistoryMaps")]
        public virtual Student Student { get; set; }
    }
}
