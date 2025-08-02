namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DocumentReferenceTypes", Schema = "mutual")]
    public partial class DocumentReferenceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentReferenceType()
        {
            //DocumentReferenceTicketStatusMaps = new HashSet<DocumentReferenceTicketStatusMap>();
            DocumentReferenceStatusMaps = new HashSet<DocumentReferenceStatusMap>();
            DocumentTypes = new HashSet<DocumentType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReferenceTypeID { get; set; }

        [StringLength(50)]
        public string InventoryTypeName { get; set; }

        [StringLength(20)]
        public string System { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DocumentReferenceTicketStatusMap> DocumentReferenceTicketStatusMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
