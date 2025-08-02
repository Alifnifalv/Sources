using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentDailyPickLogView
    {
        public long StudentPickLogIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PickDate { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(502)]
        public string Student { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string PickupBy { get; set; }
        [Required]
        [StringLength(152)]
        [Unicode(false)]
        public string PickerName { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string Status { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public long? StudentID { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(555)]
        public string MarkedBy { get; set; }
    }
}
