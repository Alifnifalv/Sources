using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Supplier_Final_Mgr
    {
        public double? AccountID { get; set; }
        public double? AccountCode { get; set; }
        [StringLength(255)]
        public string AccountName { get; set; }
        [StringLength(255)]
        public string Alias { get; set; }
        [StringLength(255)]
        public string Mrged_Account_ID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MogrSys_AccountCode_C1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MogrSys_AccountCode_C2 { get; set; }
    }
}
