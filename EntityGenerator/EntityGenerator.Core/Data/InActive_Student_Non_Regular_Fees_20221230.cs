using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class InActive_Student_Non_Regular_Fees_20221230
    {
        [StringLength(50)]
        public string FeeName { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public int? FeeMasterID { get; set; }
        public long? StudentId { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Due_Amount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal Coll_Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Balance { get; set; }
    }
}
