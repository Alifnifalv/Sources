using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Vouchers", Schema = "inventory")]
    public partial class Voucher
    {
        [Key]
        public long VoucherIID { get; set; }
        [StringLength(200)]
        public string VoucherNo { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string VoucherPin { get; set; }
        public byte? VoucherTypeID { get; set; }
        public bool? IsSharable { get; set; }
        public long? CustomerID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CurrentBalance { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public byte? StatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("Vouchers")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Vouchers")]
        public virtual VoucherStatus Status { get; set; }
        [ForeignKey("VoucherTypeID")]
        [InverseProperty("Vouchers")]
        public virtual VoucherType VoucherType { get; set; }
    }
}
