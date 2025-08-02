namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentMasterVisa")]
    public partial class PaymentMasterVisa
    {
        [Key]
        public long TrackIID { get; set; }

        public long TrackKey { get; set; }

        public long? CustomerID { get; set; }

        public long? PaymentID { get; set; }

        public DateTime? InitOn { get; set; }

        [StringLength(10)]
        public string InitStatus { get; set; }

        [StringLength(50)]
        public string InitIP { get; set; }

        [StringLength(100)]
        public string InitLocation { get; set; }

        public string VpcURL { get; set; }

        [StringLength(50)]
        public string VpcVersion { get; set; }

        [StringLength(50)]
        public string VpcCommand { get; set; }

        [StringLength(50)]
        public string AccessCode { get; set; }

        [StringLength(50)]
        public string MerchantID { get; set; }

        [StringLength(50)]
        public string VpcLocale { get; set; }

        public decimal? PaymentAmount { get; set; }

        public int? VirtualAmount { get; set; }

        [StringLength(50)]
        public string PaymentCurrency { get; set; }

        public DateTime? ResponseOn { get; set; }

        [StringLength(50)]
        public string ResponseCode { get; set; }

        [StringLength(255)]
        public string CodeDescription { get; set; }

        [StringLength(255)]
        public string Message { get; set; }

        [StringLength(50)]
        public string ReceiptNumber { get; set; }

        [StringLength(50)]
        public string TransID { get; set; }

        [StringLength(50)]
        public string AcquireResponseCode { get; set; }

        [StringLength(50)]
        public string BankAuthorizationID { get; set; }

        [StringLength(50)]
        public string BatchNo { get; set; }

        [StringLength(20)]
        public string CardType { get; set; }

        public int? ResponseAmount { get; set; }

        [StringLength(50)]
        public string ResponseIP { get; set; }

        [StringLength(100)]
        public string ResponseLocation { get; set; }

        public long? OrderID { get; set; }

        public long? CartID { get; set; }

        public string Response { get; set; }

        [StringLength(30)]
        public string LogType { get; set; }

        public int? CardTypeID { get; set; }

        public virtual CardType CardType1 { get; set; }
    }
}
