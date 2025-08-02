using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ParentUploadDocumentMaps", Schema = "schools")]
    public partial class ParentUploadDocumentMap
    {
        [Key]
        public long ParentUploadDocumentMapIID { get; set; }
        public byte? UploadDocumentTypeID { get; set; }
        public long? UploadDocumentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? ParentID { get; set; }

        [ForeignKey("ParentID")]
        [InverseProperty("ParentUploadDocumentMaps")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("UploadDocumentID")]
        [InverseProperty("ParentUploadDocumentMaps")]
        public virtual UploadDocument UploadDocument { get; set; }
        [ForeignKey("UploadDocumentTypeID")]
        [InverseProperty("ParentUploadDocumentMaps")]
        public virtual UploadDocumentType UploadDocumentType { get; set; }
    }
}
