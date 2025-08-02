using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableExtClasses", Schema = "schools")]
    public partial class TimeTableExtClass
    {
        [Key]
        public long TimeTableExtClassIID { get; set; }
        public long TimeTableExtID { get; set; }
        public int ClassID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("TimeTableExtClasses")]
        public virtual Class Class { get; set; }
        [ForeignKey("TimeTableExtID")]
        [InverseProperty("TimeTableExtClasses")]
        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
