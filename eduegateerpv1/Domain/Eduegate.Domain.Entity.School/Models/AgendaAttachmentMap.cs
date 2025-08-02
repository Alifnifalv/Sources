namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }
    }
}