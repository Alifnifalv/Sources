using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WRONG_TRANPORT_FEE_DUE_20240613
    {
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentFeeDueIID { get; set; }
        public int? StudentIID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AdmissionNumber { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string StudentName { get; set; }
        public int? FeeMasterID { get; set; }
        [StringLength(203)]
        [Unicode(false)]
        public string FeeName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? InvAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CrnAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? RecAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? OthAmount { get; set; }
    }
}
