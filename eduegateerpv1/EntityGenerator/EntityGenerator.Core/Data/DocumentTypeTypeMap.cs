using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentTypeTypeMaps", Schema = "mutual")]
    public partial class DocumentTypeTypeMap
    {
        [Key]
        public long DocumentTypeTypeMapIID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? DocumentTypeMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("DocumentTypeID")]
        [InverseProperty("DocumentTypeTypeMapDocumentTypes")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("DocumentTypeMapID")]
        [InverseProperty("DocumentTypeTypeMapDocumentTypeMaps")]
        public virtual DocumentType DocumentTypeMap { get; set; }
    }
}
