using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CircularSearch
    {
        public long CircularIID { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(50)]
        public string CircularCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CircularDate { get; set; }
        public byte? CircularPriorityID { get; set; }
        public byte? CircularStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        public byte? CircularTypeID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string ShortTitle { get; set; }
        public string Message { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
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
        [StringLength(100)]
        public string AcadamicYear { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
    }
}
