using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WRONG_BOOK_SALES_DUE_20240329
    {
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public byte? FeeCycleID { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        public int? FeeMasterID { get; set; }
        public long? FeeCollectionID { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fee_Due { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
    }
}
