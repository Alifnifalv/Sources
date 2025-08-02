using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StreamSearchView
    {
        public byte StreamID { get; set; }
        [StringLength(50)]
        public string code { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public byte? SchoolID { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
