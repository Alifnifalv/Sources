namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ParentUploadDocumentMaps", Schema = "schools")]
    public partial class ParentUploadDocumentMap
    {
        [Key]
        public long ParentUploadDocumentMapIID { get; set; }

        public byte? UploadDocumentTypeID { get; set; }

        public long? UploadDocumentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? ParentID { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual UploadDocument UploadDocument { get; set; }

        public virtual UploadDocumentType UploadDocumentType { get; set; }
    }
}
