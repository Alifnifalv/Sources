namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Attachments")]
    public partial class Attachment
    {
        [Key]
        public long AttachmentIID { get; set; }

        public int EntityTypeID { get; set; }

        public long ReferenceID { get; set; }

        [Required]
        public string AttachmentName { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? DepartmentID { get; set; }

        public virtual EntityType EntityType { get; set; }
    }
}
