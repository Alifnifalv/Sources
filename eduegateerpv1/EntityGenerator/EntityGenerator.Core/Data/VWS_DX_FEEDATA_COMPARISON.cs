using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_FEEDATA_COMPARISON
    {
        public byte FeeCycleID { get; set; }
        [StringLength(50)]
        public string Cycle { get; set; }
        public int FeeType { get; set; }
        [Required]
        [StringLength(15)]
        [Unicode(false)]
        public string FeeTypeName { get; set; }
        public long? FeeCollectionIID { get; set; }
        public long Ext_Ref_ID { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long? FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentID { get; set; }
        public int? YearID { get; set; }
        public int? MonthID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DocDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? YearMonthDate { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount_Due { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? Amount_Col { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? Amount_Crn { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? Amount_Stl { get; set; }
        public byte SchoolID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodTo { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string Term { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PrvAmount { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? CurAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PrvAmount_Due { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CurAmount_Due { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PrvAmount_Col { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? CurAmount_Col { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PrvAmount_Crn { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? CurAmount_Crn { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PrvAmount_Stl { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? CurAmount_Stl { get; set; }
    }
}
