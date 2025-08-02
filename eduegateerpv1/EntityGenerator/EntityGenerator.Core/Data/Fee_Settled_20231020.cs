using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Settled_20231020
    {
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long StudentFeeDueIID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? FeeAmount { get; set; }
    }
}
