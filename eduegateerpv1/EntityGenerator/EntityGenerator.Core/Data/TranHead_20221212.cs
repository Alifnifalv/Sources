using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TranHead_20221212
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long? StudentId { get; set; }
        public long StudentFeeDueIID { get; set; }
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
    }
}
