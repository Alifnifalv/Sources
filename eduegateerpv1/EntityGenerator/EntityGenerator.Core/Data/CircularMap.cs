using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularMaps", Schema = "schools")]
    [Index("CircularID", Name = "IDX_CircularMaps_CircularID_")]
    [Index("CircularID", Name = "IDX_CircularMaps_CircularID_ClassID__SectionID__DepartmentID__CreatedBy__CreatedDate__UpdatedBy__Up")]
    public partial class CircularMap
    {
        [Key]
        public long CircularMapIID { get; set; }
        public long? CircularID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? DepartmentID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? AllClass { get; set; }
        public bool? AllSection { get; set; }
        public bool? AllDepartment { get; set; }

        [ForeignKey("CircularID")]
        [InverseProperty("CircularMaps")]
        public virtual Circular Circular { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("CircularMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("CircularMaps")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("CircularMaps")]
        public virtual Section Section { get; set; }
    }
}
