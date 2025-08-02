using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Due_Wrong_KG_20220404
    {
        public long? StudentId { get; set; }
        public byte? SchoolID { get; set; }
        public int? ClassId { get; set; }
        public int? SectionID { get; set; }
        public int? AcadamicYearID { get; set; }
        public int FeeMasterID { get; set; }
        public int FeePeriodID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? FeeDue { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
    }
}
