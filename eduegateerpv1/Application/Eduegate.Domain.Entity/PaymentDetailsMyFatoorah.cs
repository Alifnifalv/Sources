namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentDetailsMyFatoorah")]
    public partial class PaymentDetailsMyFatoorah
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TrackID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TrackKey { get; set; }

        public long CustomerID { get; set; }

        public DateTime InitOn { get; set; }

        [Required]
        [StringLength(1)]
        public string InitStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string InitIP { get; set; }

        [Required]
        [StringLength(100)]
        public string InitLocation { get; set; }

        public decimal PaymentAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string PaymentCurrency { get; set; }

        public string InitPaymentPage { get; set; }

        [StringLength(255)]
        public string InitErrorMsg { get; set; }

        public Guid InitReference { get; set; }

        [Required]
        [StringLength(255)]
        public string ReferenceID { get; set; }

        public DateTime? TransOn { get; set; }

        [StringLength(100)]
        public string TransResult { get; set; }

        [StringLength(50)]
        public string TransRef { get; set; }

        [StringLength(255)]
        public string TransErrorMsg { get; set; }

        [StringLength(50)]
        public string TransIP { get; set; }

        [StringLength(100)]
        public string TransLocation { get; set; }

        public long? OrderID { get; set; }

        public long? CartID { get; set; }

        public long? AppKey { get; set; }

        [StringLength(50)]
        public string PaymentMode { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(50)]
        public string PayRequestResponseCode { get; set; }

        [StringLength(500)]
        public string PaymentURL { get; set; }

        public int? InvoiceNumber { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_AuthID { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_OrderID { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_PayTxnID { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_Paymode { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_PostDate { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_RefID { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_ResponseCode { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_ResponseMessage { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_TransID { get; set; }

        public decimal? OrderStatusResponse_GrossAmount { get; set; }

        public decimal? OrderStatusResponse_NetAmount { get; set; }

        [StringLength(50)]
        public string OrderStatusResponse_Result { get; set; }

        [StringLength(50)]
        public string udf1 { get; set; }

        [StringLength(50)]
        public string udf2 { get; set; }

        [StringLength(50)]
        public string udf3 { get; set; }

        [StringLength(50)]
        public string udf4 { get; set; }

        [StringLength(50)]
        public string udf5 { get; set; }
    }
}
