namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Tranhead")]
    public partial class Tranhead
    {
        [Key]
        public long TH_ID { get; set; }

        public long? InvTH_ID { get; set; }

        public int? TranNo { get; set; }

        public int? DocumentTypeID { get; set; }

        public int? FiscalYear_ID { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? TranDate { get; set; }

        [StringLength(50)]
        public string VoucherNo { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        public bool? IsPosted { get; set; }

        public bool? IsDeleted { get; set; }

        public int? Ref_ID { get; set; }

        public int? Session_ID { get; set; }

        public int? PostedBy { get; set; }

        public DateTime? PostedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? Station_ID { get; set; }

        public int? AccountTransactionHeadID { get; set; }

        public int? CFC_ID { get; set; }

        public bool? IsReversed { get; set; }

        public DateTime? ReversedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamps { get; set; }

        public long? Ext_Ref_ID { get; set; }

        public long? Ext_Ref_Map_ID { get; set; }

        public long? Ext_Ref_Split_ID { get; set; }

        public int? Ext_Ref_Month_ID { get; set; }

        public int? Ext_Ref_Year_ID { get; set; }
    }
}
