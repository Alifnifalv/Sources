using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AssignmentsSearch
    {
        public long AssignmentIID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        public int? SectionID { get; set; }
        [StringLength(4000)]
        public string Section { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
        public byte? AssignmentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateOfSubmission { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StartDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FreezeDate { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
