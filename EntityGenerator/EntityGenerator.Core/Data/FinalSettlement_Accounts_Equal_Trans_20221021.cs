using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FinalSettlement_Accounts_Equal_Trans_20221021
    {
        public long New_TH_ID { get; set; }
        public long Old_TH_ID { get; set; }
    }
}
