using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BranchDocumentTypeMaps", Schema = "inventory")]
    public partial class BranchDocumentTypeMap
    {
        [Key]
        public long BranchDocumentTypeMapIID { get; set; }
        public long? BranchID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("BranchDocumentTypeMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("BranchDocumentTypeMaps")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
