using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Accounts_SubLedger_Relation_20230910
    {
        public long SL_Rln_ID { get; set; }
        public long AccountID { get; set; }
        public long SL_AccountID { get; set; }
    }
}
