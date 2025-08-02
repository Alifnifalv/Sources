using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AccountsSubLedgerSearch
    {
        public long SL_AccountID { get; set; }
        [StringLength(50)]
        public string SL_AccountCode { get; set; }
        [StringLength(100)]
        public string SL_AccountName { get; set; }
        [StringLength(100)]
        public string SL_Alias { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string AllowUserDelete { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string AllowUserEdit { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string AllowUserRename { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsHidden { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Balance { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
