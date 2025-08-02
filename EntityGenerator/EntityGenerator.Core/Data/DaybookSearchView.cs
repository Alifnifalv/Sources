using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DaybookSearchView
    {
        public long TransactionIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        [Column("Ref#")]
        [StringLength(500)]
        public string Ref_ { get; set; }
        [StringLength(100)]
        public string CostCenterName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string DebitOrCredit { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string DocumentName { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
    }
}
