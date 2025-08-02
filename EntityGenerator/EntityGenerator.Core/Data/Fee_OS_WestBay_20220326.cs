using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_OS_WestBay_20220326
    {
        [Column("Fee Name")]
        [StringLength(255)]
        public string Fee_Name { get; set; }
        [Column("Invoice Date", TypeName = "datetime")]
        public DateTime? Invoice_Date { get; set; }
        [Column("Invoice No#")]
        [StringLength(255)]
        public string Invoice_No_ { get; set; }
        [StringLength(255)]
        public string OP_Fee_Due { get; set; }
        [StringLength(255)]
        public string OP_Fee_Crn { get; set; }
        [StringLength(255)]
        public string OP_Fee_Rec { get; set; }
        [StringLength(255)]
        public string OP_Total { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_Fee_Due { get; set; }
        [StringLength(255)]
        public string TR_Fee_Crn { get; set; }
        [StringLength(255)]
        public string TR_Fee_Rec { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_Total { get; set; }
        [StringLength(255)]
        public string CL_Settled { get; set; }
        [Column(TypeName = "money")]
        public decimal? CL_Total { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [StringLength(255)]
        public string Remarks1 { get; set; }
        [StringLength(255)]
        public string Remarks2 { get; set; }
        [StringLength(255)]
        public string Remarks3 { get; set; }
        [StringLength(255)]
        public string Remarks4 { get; set; }
    }
}
