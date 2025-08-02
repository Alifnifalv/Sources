using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssignmentSectionMaps", Schema = "schools")]
    [Index("AssignmentID", Name = "IDX_AssignmentSectionMaps_AssignmentID_")]
    [Index("AssignmentID", Name = "IDX_AssignmentSectionMaps_AssignmentID_SectionID__CreatedBy__UpdatedBy__CreatedDate__UpdatedDate")]
    [Index("AssignmentID", "SectionID", Name = "IDX_AssignmentSectionMaps_AssignmentID__SectionID_")]
    public partial class AssignmentSectionMap
    {
        [Key]
        public long AssignmentSectionMapIID { get; set; }
        public long? AssignmentID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AssignmentID")]
        [InverseProperty("AssignmentSectionMaps")]
        public virtual Assignment Assignment { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("AssignmentSectionMaps")]
        public virtual Section Section { get; set; }
    }
}
