using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Sub_LedgerSummary_20231231
    {
        public int? RW { get; set; }
        public long AccountID { get; set; }
        public int? CompanyID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public int SL_AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public int CP_AccountID { get; set; }
    }
}
