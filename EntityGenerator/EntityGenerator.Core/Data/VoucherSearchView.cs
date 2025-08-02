using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VoucherSearchView
    {
        public long VoucherIID { get; set; }
        [StringLength(200)]
        public string VoucherNo { get; set; }
        public byte? VoucherTypeID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(50)]
        public string VoucherTypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? createddate { get; set; }
        public bool? IsSharable { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        public long? CustomerIID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RemainingAmount { get; set; }
        [Column(TypeName = "decimal(19, 3)")]
        public decimal? UsedAmount { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        public long? CustomerID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumAmount { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public byte? StatusID { get; set; }
        public int? CompanyID { get; set; }
        [Required]
        public string SalesOrder { get; set; }
    }
}
