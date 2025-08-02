using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Due_Duplicated_20230125
    {
        public long? RowIndex { get; set; }
        public long? StudentId { get; set; }
        public int? FeeMasterID { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        public int? CNT { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
    }
}
