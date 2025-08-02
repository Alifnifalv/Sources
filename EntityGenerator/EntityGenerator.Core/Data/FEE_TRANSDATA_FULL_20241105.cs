using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FEE_TRANSDATA_FULL_20241105
    {
        public bool IsCancelled { get; set; }
        public int DocumentTypeID { get; set; }
        public long Ext_Ref_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        public long? StudentID { get; set; }
        public long? FeeDueFeeTypeMapsIID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount { get; set; }
    }
}
