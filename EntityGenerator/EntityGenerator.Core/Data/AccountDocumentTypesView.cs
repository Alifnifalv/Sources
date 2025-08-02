using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AccountDocumentTypesView
    {
        public int DocumentTypeID { get; set; }
        public int? ReferenceTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InventoryTypeName { get; set; }
        public int? CompanyID { get; set; }
    }
}
