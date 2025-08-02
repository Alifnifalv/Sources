using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WRONG_COLLECTED_SPLIT_NOT_FOUND_FEE_DUE_20220621
    {
        public long? StudentId { get; set; }
        public int? AcadamicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? ClassId { get; set; }
        public int? SectionID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
    }
}
