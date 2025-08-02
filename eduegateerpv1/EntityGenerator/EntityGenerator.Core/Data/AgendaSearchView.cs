using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AgendaSearchView
    {
        public long AgendaIID { get; set; }
        public int? ReferenceID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(50)]
        public string LectureCode { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(4000)]
        public string SectionName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public byte? AgendaStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
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
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
