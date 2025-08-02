using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CounselorHubsearchView
    {
        public long CounselorHubIID { get; set; }
        public byte? SchoolID { get; set; }
        public byte? CounselorHubStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CounselorHubEntryDate { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string ShortTitle { get; set; }
        public string Message { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
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
