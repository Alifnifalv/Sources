using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassClassGroupMaps", Schema = "schools")]
    public partial class ClassClassGroupMap
    {
        [Key]
        public long ClassClassGroupMapIID { get; set; }
        public long? ClassGroupID { get; set; }
        public int? ClassID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassClassGroupMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassClassGroupMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("ClassGroupID")]
        [InverseProperty("ClassClassGroupMaps")]
        public virtual ClassGroup ClassGroup { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassClassGroupMaps")]
        public virtual School School { get; set; }
    }
}
