using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CampusTransfersSearchView
    {
        public long CampusTransferIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransferDate { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public long StudentID { get; set; }
        [StringLength(103)]
        public string FromClass { get; set; }
        [StringLength(103)]
        public string Toclass { get; set; }
        [StringLength(50)]
        public string FromSection { get; set; }
        [StringLength(50)]
        public string ToSection { get; set; }
        [StringLength(100)]
        public string FromAcademicYear { get; set; }
        [StringLength(100)]
        public string ToAcademicYear { get; set; }
        public byte? ToSchoolID { get; set; }
        public int ToClassID { get; set; }
        public int ToSectionID { get; set; }
        public int ToAcademicYearID { get; set; }
    }
}
