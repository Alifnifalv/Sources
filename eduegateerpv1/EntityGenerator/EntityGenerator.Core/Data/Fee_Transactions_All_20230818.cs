using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Transactions_All_20230818
    {
        public int IsOpening { get; set; }
        public int DcoumentTypeID { get; set; }
        public long AccountID { get; set; }
        public bool IsCancelled { get; set; }
        public bool CollectionStatus { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DocDate { get; set; }
        [StringLength(50)]
        public string DocNo { get; set; }
        public int? FeeMasterID { get; set; }
        public bool? IsActive { get; set; }
        public byte? Status { get; set; }
        public long? StudentId { get; set; }
        public long? SL_AccountID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fee_Amount { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long? Ext_Ref_MapID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_TranDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
    }
}
