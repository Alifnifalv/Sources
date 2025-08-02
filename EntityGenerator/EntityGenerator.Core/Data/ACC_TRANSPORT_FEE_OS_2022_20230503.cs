using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ACC_TRANSPORT_FEE_OS_2022_20230503
    {
        public long AccountID { get; set; }
        public int? CompanyID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public int SL_AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Col_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Crn_Amount { get; set; }
    }
}
