using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentDetailsKnet", Schema = "payment")]
    public partial class PaymentDetailsKnet
    {
        [Key]
        public long TrackID { get; set; }
        [Key]
        public long TrackKey { get; set; }
        public long CustomerID { get; set; }
        public long PaymentID { get; set; }
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
        [StringLength(1)]
        [Unicode(false)]
        public string PaymentAction { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string PaymentCurrency { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string PaymentLang { get; set; }
        public string InitRawResponse { get; set; }
        public string InitPaymentPage { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string InitErrorMsg { get; set; }
        public long? TransID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransOn { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string TransResult { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransPostDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransAuth { get; set; }
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
    }
}
