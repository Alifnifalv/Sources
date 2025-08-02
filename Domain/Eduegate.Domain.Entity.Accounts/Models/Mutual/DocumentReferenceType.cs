using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("DocumentReferenceTypes", Schema = "mutual")]
    public partial class DocumentReferenceType
    {
        public DocumentReferenceType()
        {
            DocumentReferenceStatusMaps = new HashSet<DocumentReferenceStatusMap>();
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

        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }

        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}