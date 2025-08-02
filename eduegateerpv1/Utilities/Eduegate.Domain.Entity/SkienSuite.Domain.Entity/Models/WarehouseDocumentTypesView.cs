using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WarehouseDocumentTypesView
    {
        public int DocumentTypeID { get; set; }
        public Nullable<int> ReferenceTypeID { get; set; }
        public string TransactionTypeName { get; set; }
        public string InventoryTypeName { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
