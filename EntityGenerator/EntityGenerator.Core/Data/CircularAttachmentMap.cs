using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularAttachmentMaps", Schema = "schools")]
    [Index("CircularID", Name = "IDX_CircularAttachmentMaps_CircularID_")]
    [Index("CircularID", Name = "IDX_CircularAttachmentMaps_CircularID_AttachmentReferenceID__AttachmentName__AttachmentDescription_")]
    public partial class CircularAttachmentMap
    {
        [Key]
        public long CircularAttachmentMapIID { get; set; }
        public long? CircularID { get; set; }
        public long? AttachmentReferenceID { get; set; }
        [StringLength(150)]
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

        [ForeignKey("CircularID")]
        [InverseProperty("CircularAttachmentMaps")]
        public virtual Circular Circular { get; set; }
    }
}
