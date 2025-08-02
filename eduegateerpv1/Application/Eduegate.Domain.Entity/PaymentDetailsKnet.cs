namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentDetailsKnet")]
    public partial class PaymentDetailsKnet
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

        public long PaymentID { get; set; }

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
        [StringLength(1)]
        public string PaymentAction { get; set; }

        [Required]
        [StringLength(3)]
        public string PaymentCurrency { get; set; }

        [Required]
        [StringLength(3)]
        public string PaymentLang { get; set; }

        public string InitRawResponse { get; set; }

        public string InitPaymentPage { get; set; }

        [StringLength(255)]
        public string InitErrorMsg { get; set; }

        public long? TransID { get; set; }

        public DateTime? TransOn { get; set; }

        [StringLength(100)]
        public string TransResult { get; set; }

        [StringLength(50)]
        public string TransPostDate { get; set; }

        [StringLength(50)]
        public string TransAuth { get; set; }

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
    }
}
