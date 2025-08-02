using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PaymentLogSearchView
    {
        [Required]
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [Required]
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        public long PaymentLogIID { get; set; }
        public long? TrackID { get; set; }
        public long? TrackKey { get; set; }
        public string RequestLog { get; set; }
        [StringLength(50)]
        public string RequestType { get; set; }
        [Required]
        public string ResponseLog { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }
        public string RequestUrl { get; set; }
        public long? CartID { get; set; }
        public long? CustomerID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string TransNo { get; set; }
        [StringLength(100)]
        public string LoginUserID { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        [StringLength(302)]
        public string MotherName { get; set; }
        [StringLength(302)]
        public string GuardianName { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public int? CardTypeID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string CardType { get; set; }
    }
}
