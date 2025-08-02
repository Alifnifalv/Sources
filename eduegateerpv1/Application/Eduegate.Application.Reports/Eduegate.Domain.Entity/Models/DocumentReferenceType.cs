using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentReferenceType
    {
        public DocumentReferenceType()
        {
            this.DocumentReferenceStatusMaps = new List<DocumentReferenceStatusMap>();
            this.DocumentTypes = new List<DocumentType>();
        }

        public int ReferenceTypeID { get; set; }
        public string InventoryTypeName { get; set; }
        public string System { get; set; }
        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
