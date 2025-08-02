using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentReferenceTypes", Schema = "mutual")]
    public partial class DocumentReferenceType
    {
        public DocumentReferenceType()
        {
            DocumentReferenceStatusMaps = new HashSet<DocumentReferenceStatusMap>();
            DocumentReferenceTicketStatusMaps = new HashSet<DocumentReferenceTicketStatusMap>();
            DocumentTypes = new HashSet<DocumentType>();
        }

        [Key]
        public int ReferenceTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InventoryTypeName { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string System { get; set; }

        [InverseProperty("ReferenceType")]
        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        [InverseProperty("ReferenceType")]
        public virtual ICollection<DocumentReferenceTicketStatusMap> DocumentReferenceTicketStatusMaps { get; set; }
        [InverseProperty("ReferenceType")]
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
