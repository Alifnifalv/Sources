using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TrialBalancePMS_Mgr
    {
        [Column(" Account Code")]
        public double? _Account_Code { get; set; }
        [Column(" Account Name")]
        [StringLength(255)]
        public string _Account_Name { get; set; }
        [Column("  Opening Balance")]
        public double? __Opening_Balance { get; set; }
        [Column("  Opening Balance1")]
        public double? __Opening_Balance1 { get; set; }
        [Column("  Transaction Amount")]
        public double? __Transaction_Amount { get; set; }
        [Column("  Transaction Amount1")]
        public double? __Transaction_Amount1 { get; set; }
        [Column("  Closing Balance")]
        public double? __Closing_Balance { get; set; }
        [Column("  Closing Balance1")]
        public double? __Closing_Balance1 { get; set; }
        public double? Balance { get; set; }
    }
}
