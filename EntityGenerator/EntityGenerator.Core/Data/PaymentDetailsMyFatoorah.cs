using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentDetailsMyFatoorah", Schema = "payment")]
    public partial class PaymentDetailsMyFatoorah
    {
        [Key]
        public long TrackID { get; set; }
        [Key]
        public long TrackKey { get; set; }
        public long CustomerID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InitOn { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string InitStatus { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string InitIP { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string InitLocation { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal PaymentAmount { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string PaymentCurrency { get; set; }
        public string InitPaymentPage { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string InitErrorMsg { get; set; }
        public Guid InitReference { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ReferenceID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransOn { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string TransResult { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransRef { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string TransErrorMsg { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransIP { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string TransLocation { get; set; }
        public long? OrderID { get; set; }
        public long? CartID { get; set; }
        public long? AppKey { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PaymentMode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PaymentMethod { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PayRequestResponseCode { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string PaymentURL { get; set; }
        public int? InvoiceNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_AuthID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_OrderID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_PayTxnID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_Paymode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_PostDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_RefID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_ResponseCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_ResponseMessage { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_TransID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OrderStatusResponse_GrossAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OrderStatusResponse_NetAmount { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderStatusResponse_Result { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string udf1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string udf2 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string udf3 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string udf4 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string udf5 { get; set; }
    }
}
