using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AgendaSectionMaps", Schema = "schools")]
    [Index("AgendaID", Name = "IDX_AgendaSectionMaps_AgendaID_SectionID__CreatedBy__UpdatedBy__CreatedDate__UpdatedDate")]
    [Index("SectionID", Name = "IDX_AgendaSectionMaps_SectionID_AgendaID")]
    public partial class AgendaSectionMap
    {
        [Key]
        public long AgendaSectionMapIID { get; set; }
        public long? AgendaID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AgendaID")]
        [InverseProperty("AgendaSectionMaps")]
        public virtual Agenda Agenda { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("AgendaSectionMaps")]
        public virtual Section Section { get; set; }
    }
}
