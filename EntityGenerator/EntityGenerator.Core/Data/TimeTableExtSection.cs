using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableExtSections", Schema = "schools")]
    public partial class TimeTableExtSection
    {
        [Key]
        public long TimeTableExtSectionIID { get; set; }
        public long TimeTableExtID { get; set; }
        public int? ClassID { get; set; }
        public int SectionID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("TimeTableExtSections")]
        public virtual Class Class { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("TimeTableExtSections")]
        public virtual Section Section { get; set; }
        [ForeignKey("TimeTableExtID")]
        [InverseProperty("TimeTableExtSections")]
        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
