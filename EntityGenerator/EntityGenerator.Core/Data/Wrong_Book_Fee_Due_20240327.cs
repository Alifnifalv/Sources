using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Wrong_Book_Fee_Due_20240327
    {
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentId { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int FeeDueAmount { get; set; }
        public int FeeDue_StudentFeeDueID { get; set; }
        public int FeeDue_FeeDueFeeTypeMapsIID { get; set; }
    }
}
