using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StockDocumentTypesView
    {
        public int DocumentTypeID { get; set; }
        public int? ReferenceTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InventoryTypeName { get; set; }
        public long? CreatedBy { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        public long? UpdatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
