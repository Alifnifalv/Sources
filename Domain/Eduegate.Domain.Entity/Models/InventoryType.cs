using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class InventoryType
    {
        public InventoryType()
        {
            this.DocumentTypes = new List<DocumentType>();
        }

        [Key]
        public int InventoryTypeID { get; set; }
        public string InventoryTypeName { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
