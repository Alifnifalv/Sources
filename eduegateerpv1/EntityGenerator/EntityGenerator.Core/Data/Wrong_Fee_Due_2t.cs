using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Wrong_Fee_Due_2t
    {
        public long? StudentId { get; set; }
        public int? FeePeriodID { get; set; }
        public int? AcadamicYearID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string FeePeriod { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? CollAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Balance { get; set; }
    }
}
