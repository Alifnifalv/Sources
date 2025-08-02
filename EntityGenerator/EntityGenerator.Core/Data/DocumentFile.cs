using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentFiles", Schema = "doc")]
    public partial class DocumentFile
    {
        [Key]
        public long DocumentFileIID { get; set; }
        [Required]
        [Unicode(false)]
        public string FileName { get; set; }
        public long EntityTypeID { get; set; }
        public long? ReferenceID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Title { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Tags { get; set; }
        [StringLength(1000)]
        public string ActualFileName { get; set; }
        public int? DocFileTypeID { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string Description { get; set; }
        public long? OwnerEmployeeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Version { get; set; }
        public long? DocumentStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public string ExtractedData { get; set; }
        public string ExtractedHeaderData { get; set; }
        public string ExtractedGridData { get; set; }

        [ForeignKey("DocFileTypeID")]
        [InverseProperty("DocumentFiles")]
        public virtual DocFileType DocFileType { get; set; }
        [ForeignKey("DocumentStatusID")]
        [InverseProperty("DocumentFiles")]
        public virtual DocumentFileStatus DocumentStatus { get; set; }
        [ForeignKey("OwnerEmployeeID")]
        [InverseProperty("DocumentFiles")]
        public virtual Employee OwnerEmployee { get; set; }
    }
}
