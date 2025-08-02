namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("doc.DocumentFiles")]
    public partial class DocumentFile
    {
        [Key]
        public long DocumentFileIID { get; set; }

        [Required]
        public string FileName { get; set; }

        public long EntityTypeID { get; set; }

        public long? ReferenceID { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Tags { get; set; }

        [StringLength(1000)]
        public string ActualFileName { get; set; }

        public int? DocFileTypeID { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public long? OwnerEmployeeID { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        public long? DocumentStatusID { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual DocFileType DocFileType { get; set; }

        public virtual DocumentFileStatus DocumentFileStatus { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
