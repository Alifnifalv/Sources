using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Accounts_SubLedger_20220513
    {
        public long SL_AccountID { get; set; }
        [StringLength(50)]
        public string SL_AccountCode { get; set; }
        [StringLength(100)]
        public string SL_AccountName { get; set; }
        [StringLength(100)]
        public string SL_Alias { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsHidden { get; set; }
        public bool? AllowUserDelete { get; set; }
        public bool? AllowUserEdit { get; set; }
        public bool? AllowUserRename { get; set; }
    }
}
