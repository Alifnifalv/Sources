namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.UploadDocuments")]
    public partial class UploadDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UploadDocument()
        {
            ParentUploadDocumentMaps = new HashSet<ParentUploadDocumentMap>();
        }

        [Key]
        public long UploadDocumentIID { get; set; }

        public int? ContentTypeID { get; set; }

        [StringLength(50)]
        public string DocumentFileName { get; set; }

        public byte[] ContentData { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(25)]
        public string ReferenceID { get; set; }

        public byte? UploadDocumentTypeID { get; set; }

        public virtual ContentType ContentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParentUploadDocumentMap> ParentUploadDocumentMaps { get; set; }

        public virtual UploadDocumentType UploadDocumentType { get; set; }
    }
}
