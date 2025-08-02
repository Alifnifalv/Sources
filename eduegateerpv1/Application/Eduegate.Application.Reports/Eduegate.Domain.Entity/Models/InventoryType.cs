using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class InventoryType
    {
        public InventoryType()
        {
            this.DocumentTypes = new List<DocumentType>();
        }

        public int InventoryTypeID { get; set; }
        public string InventoryTypeName { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
