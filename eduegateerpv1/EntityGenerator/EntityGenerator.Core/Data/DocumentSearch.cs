using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DocumentSearch
    {
        public long DocumentFileIID { get; set; }
        [StringLength(1000)]
        public string FileName { get; set; }
        [Required]
        [Unicode(false)]
        public string PhysicalFileName { get; set; }
        public long EntityTypeID { get; set; }
        public long? ReferenceID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Title { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Tags { get; set; }
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
        [StringLength(10)]
        public string StatusName { get; set; }
        [StringLength(150)]
        public string EntityName { get; set; }
        public int? DocFileTypeID { get; set; }
        [StringLength(50)]
        public string FileTypeName { get; set; }
        [Required]
        [StringLength(73)]
        public string FileTypeImage { get; set; }
        [StringLength(100)]
        public string FileTypeDescription { get; set; }
        public int CommentCounts { get; set; }
    }
}
