using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TransactionHeadEntitlementMap416736", Schema = "inventory")]
    public partial class TransactionHeadEntitlementMap416736
    {
        public long TransactionHeadEntitlementMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public byte EntitlementID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(25)]
        public string ReferenceNo { get; set; }
    }
}
