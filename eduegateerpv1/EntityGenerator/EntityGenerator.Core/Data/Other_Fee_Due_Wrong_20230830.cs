using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Other_Fee_Due_Wrong_20230830
    {
        public int FeeType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public long TH_ID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public int? FeeMasterID { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentId { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? SL_AccountID { get; set; }
    }
}
