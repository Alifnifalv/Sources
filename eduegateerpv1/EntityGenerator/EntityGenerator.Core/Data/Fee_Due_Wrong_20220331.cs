using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Due_Wrong_20220331
    {
        public byte SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string Class_Current { get; set; }
        [StringLength(50)]
        public string Class_Wrong { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
    }
}
