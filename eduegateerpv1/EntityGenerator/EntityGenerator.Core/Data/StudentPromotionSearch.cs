using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentPromotionSearch
    {
        [StringLength(20)]
        public string FROMACADEMICYEAR { get; set; }
        [StringLength(20)]
        public string TOACADEMICYEAR { get; set; }
        public int ClassID { get; set; }
        [StringLength(50)]
        public string FromClass { get; set; }
        [Required]
        [StringLength(50)]
        public string FromSection { get; set; }
        public int? NumOfstudents { get; set; }
        [Required]
        [StringLength(50)]
        public string ToSection { get; set; }
        [StringLength(50)]
        public string ToClass { get; set; }
        public bool Status { get; set; }
        public string Remarks { get; set; }
        public byte? SchoolID { get; set; }
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
