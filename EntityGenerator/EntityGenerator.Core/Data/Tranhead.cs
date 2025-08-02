using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Tranhead", Schema = "account")]
    [Index("CompanyID", "IsDeleted", Name = "IDX_Tranhead_CompanyID_IsDeleted")]
    [Index("CompanyID", "IsDeleted", Name = "IDX_Tranhead_CompanyID__IsDeleted_DocumentTypeID__FiscalYear_ID__TranDate")]
    [Index("DocumentTypeID", "IsDeleted", Name = "IDX_Tranhead_DocumentTypeID_IsDeleted")]
    [Index("DocumentTypeID", "IsDeleted", Name = "IDX_Tranhead_DocumentTypeID__IsDeleted_Ext_Ref_ID__Ext_Ref_Map_ID")]
    [Index("DocumentTypeID", "IsDeleted", "Ext_Ref_ID", "Ext_Ref_Map_ID", Name = "IDX_Tranhead_DocumentTypeID__IsDeleted__Ext_Ref_ID__Ext_Ref_Map_ID_")]
    [Index("FiscalYear_ID", "IsDeleted", Name = "IDX_Tranhead_FiscalYear_ID__IsDeleted_DocumentTypeID__CompanyID__TranDate")]
    [Index("IsDeleted", Name = "IDX_Tranhead_IsDeleted")]
    [Index("IsDeleted", "TranDate", Name = "IDX_Tranhead_IsDeletedTranDate_DocumentTypeID__FiscalYear_ID__CompanyID")]
    [Index("IsDeleted", "TranDate", Name = "IDX_Tranhead_IsDeleted_TranDate")]
    [Index("TranNo", "DocumentTypeID", "CompanyID", "FiscalYear_ID", Name = "IX_Tranhead", IsUnique = true)]
    [Index("DocumentTypeID", "IsDeleted", Name = "id")]
    [Index("AccountTransactionHeadID", Name = "idx_TranheadAccountTransactionHeadID")]
    [Index("DocumentTypeID", "IsDeleted", Name = "idx_TranheadDocumentTypeIDIsDeleted")]
    [Index("IsDeleted", Name = "idx_TranheadIsDeleted")]
    public partial class Tranhead
    {
        public Tranhead()
        {
            BankReconciliationDetails = new HashSet<BankReconciliationDetail>();
            BankReconciliationMatchingEntries = new HashSet<BankReconciliationMatchingEntry>();
        }

        [Key]
        public long TH_ID { get; set; }
        public long? InvTH_ID { get; set; }
        public int? TranNo { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Ref_ID { get; set; }
        public int? Session_ID { get; set; }
        public int? PostedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PostedDate { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? DeletedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
        public int? Station_ID { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        public int? CFC_ID { get; set; }
        public bool? IsReversed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReversedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long? Ext_Ref_Map_ID { get; set; }
        public long? Ext_Ref_Split_ID { get; set; }
        public int? Ext_Ref_Month_ID { get; set; }
        public int? Ext_Ref_Year_ID { get; set; }

        [InverseProperty("TranHead")]
        public virtual ICollection<BankReconciliationDetail> BankReconciliationDetails { get; set; }
        [InverseProperty("TranHead")]
        public virtual ICollection<BankReconciliationMatchingEntry> BankReconciliationMatchingEntries { get; set; }
    }
}
