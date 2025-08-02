using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TempFEECOLLECTTransport")]
    public partial class TempFEECOLLECTTransport
    {
        public long? StudentId { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public bool CollectionStatus { get; set; }
        public bool STATUS { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DUEAMOUNT { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? dUECollectedAmount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal COLLECTEDAMT { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CREDINOTEAMT { get; set; }
        public long? credFeeDueFeeTypeMapsID { get; set; }
        public long? credFeeDueMonthlySplitID { get; set; }
    }
}
