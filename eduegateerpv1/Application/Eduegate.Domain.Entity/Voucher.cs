namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.Vouchers")]
    public partial class Voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voucher()
        {
            ShoppingCartVoucherMaps = new HashSet<ShoppingCartVoucherMap>();
        }

        [Key]
        public long VoucherIID { get; set; }

        [StringLength(200)]
        public string VoucherNo { get; set; }

        [StringLength(4)]
        public string VoucherPin { get; set; }

        public byte? VoucherTypeID { get; set; }

        public bool? IsSharable { get; set; }

        public long? CustomerID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public decimal? MinimumAmount { get; set; }

        public decimal? CurrentBalance { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public byte? StatusID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual VoucherStatus VoucherStatus { get; set; }

        public virtual VoucherType VoucherType { get; set; }
    }
}
