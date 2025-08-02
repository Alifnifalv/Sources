using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FinalSettlement_Creditnote_Closing_20230930
    {
        [Column(TypeName = "datetime")]
        public DateTime? FinalSettlementDate { get; set; }
        public long? StudentId { get; set; }
        public int? FeeMasterID { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
    }
}
