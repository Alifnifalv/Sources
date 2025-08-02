namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("doc.DocFileTypes")]
    public partial class DocFileType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocFileType()
        {
            DocumentFiles = new HashSet<DocumentFile>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocFileTypeID { get; set; }

        [StringLength(50)]
        public string FileTypeName { get; set; }

        [StringLength(5)]
        public string FileExtension { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
