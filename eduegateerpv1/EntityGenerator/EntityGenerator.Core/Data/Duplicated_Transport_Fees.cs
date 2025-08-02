using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Duplicated_Transport_Fees
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? Rw { get; set; }
        public long? StudentId { get; set; }
        public int? AcadamicYearID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public long FeeDueMonthlySplitIID { get; set; }
        public int MonthID { get; set; }
        public int? Year { get; set; }
        public bool CollectionStatus { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CollectedAmount { get; set; }
    }
}
