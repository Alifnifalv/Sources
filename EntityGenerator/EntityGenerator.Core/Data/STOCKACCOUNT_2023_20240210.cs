using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STOCKACCOUNT_2023_20240210
    {
        public int? RW { get; set; }
        public int? ACCOUNTID { get; set; }
        public int? COMPANYID { get; set; }
        [Column(TypeName = "money")]
        public decimal? OPSTOCKVAL { get; set; }
        [Column(TypeName = "money")]
        public decimal? CLSTOCKVAL { get; set; }
    }
}
