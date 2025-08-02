namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UploadDocumentTypes", Schema = "schools")]
    public partial class UploadDocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UploadDocumentType()
        {
            ParentUploadDocumentMaps = new HashSet<ParentUploadDocumentMap>();
            UploadDocuments = new HashSet<UploadDocument>();
        }
        [Key]
        public byte UploadDocumentTypeID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public bool? IsMandatory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParentUploadDocumentMap> ParentUploadDocumentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UploadDocument> UploadDocuments { get; set; }
    }
}
