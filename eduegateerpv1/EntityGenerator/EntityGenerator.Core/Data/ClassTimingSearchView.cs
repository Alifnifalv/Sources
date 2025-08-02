using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassTimingSearchView
    {
        public int ClassTimingID { get; set; }
        [StringLength(50)]
        public string TimingDescription { get; set; }
        public int? ClassTimingSetID { get; set; }
        [StringLength(50)]
        public string TimingSetName { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string IsBreakTime { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(50)]
        public string BreakTypeName { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string StartTime { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string EndTime { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
