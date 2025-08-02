using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDueCancellationView
    {
        public long FeeDueCancellationIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
    }
}
