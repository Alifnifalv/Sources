using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CRN_TRANHEAD_DATE_CHANGED_20230808
    {
        public long TH_ID { get; set; }
        public int? ACCOUNTTRANSACTIONHEADID { get; set; }
    }
}
