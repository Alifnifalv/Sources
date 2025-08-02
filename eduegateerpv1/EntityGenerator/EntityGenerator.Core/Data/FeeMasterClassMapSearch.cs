using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeMasterClassMapSearch
    {
        public long ClassFeeMasterIID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? ClassID { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
    }
}
