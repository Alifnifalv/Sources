using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SCHOO_ACCOUNT_FEE_COMPANY_CHANGES_20230831
    {
        public int ACCOUNTID { get; set; }
        public int? COMPANYID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        public long? SL_AccountID { get; set; }
        public int CHG_COMPANYID { get; set; }
    }
}
