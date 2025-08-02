using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StockInSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        public long? CustomerID { get; set; }
        public long? SupplierID { get; set; }
        [StringLength(255)]
        public string Expr1 { get; set; }
        [StringLength(255)]
        public string Expr2 { get; set; }
        [StringLength(255)]
        public string Expr3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
    }
}
