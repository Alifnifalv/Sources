using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Vouchers", Schema = "inventory")]
    public partial class Voucher
    {
        public Voucher()
        {
            this.ShoppingCartVoucherMaps = new List<ShoppingCartVoucherMap>();
        }

        [Key]
        public long VoucherIID { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherPin { get; set; }
        public Nullable<byte> VoucherTypeID { get; set; }
        public Nullable<bool> IsSharable { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<decimal> MinimumAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public string Description { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual VoucherStatus VoucherStatus { get; set; }
        public virtual VoucherType VoucherType { get; set; }
    }
}
