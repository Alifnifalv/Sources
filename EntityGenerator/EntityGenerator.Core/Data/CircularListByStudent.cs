using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CircularListByStudent
    {
        public long CircularIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CircularDate { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public long StudentID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string ShortTitle { get; set; }
        [StringLength(50)]
        public string CircularCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
    }
}
