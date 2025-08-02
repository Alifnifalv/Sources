using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShareHolders", Schema = "inventory")]
    public partial class ShareHolder
    {
        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string ShareHolderID { get; set; }
        public long? LoginID { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(50)]
        public string ShareHolderCode { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? TotalNumberOfShare { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AmountOfInvestment { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MemberShipCard { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MemberShipCardExpiry { get; set; }
        [StringLength(50)]
        public string FamilyName { get; set; }
        [StringLength(50)]
        public string NationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CreditLimit { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Balance { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("ShareHolders")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("ShareHolders")]
        public virtual Login Login { get; set; }
    }
}
