using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("DocumentReferenceTypes", Schema = "mutual")]
    public partial class DocumentReferenceType
    {
        public DocumentReferenceType()
        {
            //DocumentReferenceStatusMaps = new HashSet<DocumentReferenceStatusMap>();
            DocumentReferenceTicketStatusMaps = new HashSet<DocumentReferenceTicketStatusMap>();
            DocumentTypes = new HashSet<DocumentType>();
        }

        [Key]
        public int ReferenceTypeID { get; set; }

        [StringLength(50)]
        public string InventoryTypeName { get; set; }

        [StringLength(20)]
        public string System { get; set; }

        //public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }

        public virtual ICollection<DocumentReferenceTicketStatusMap> DocumentReferenceTicketStatusMaps { get; set; }

        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}