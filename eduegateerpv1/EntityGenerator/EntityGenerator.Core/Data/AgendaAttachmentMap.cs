using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AgendaAttachmentMaps", Schema = "schools")]
    public partial class AgendaAttachmentMap
    {
        [Key]
        public long AgendaAttachmentMapIID { get; set; }
        public long? AgendaID { get; set; }
        public long? AttachmentReferenceID { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        [StringLength(1000)]
        public string AttachmentDescription { get; set; }
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AgendaID")]
        [InverseProperty("AgendaAttachmentMaps")]
        public virtual Agenda Agenda { get; set; }
    }
}
