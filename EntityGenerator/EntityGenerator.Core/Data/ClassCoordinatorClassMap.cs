using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassCoordinatorClassMaps", Schema = "schools")]
    public partial class ClassCoordinatorClassMap
    {
        [Key]
        public long ClassCoordinatorClassMapIID { get; set; }
        public long? ClassCoordinatorID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? AllClass { get; set; }
        public bool? AllSection { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("ClassCoordinatorClassMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("ClassCoordinatorID")]
        [InverseProperty("ClassCoordinatorClassMaps")]
        public virtual ClassCoordinator ClassCoordinator { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassCoordinatorClassMaps")]
        public virtual Section Section { get; set; }
    }
}
