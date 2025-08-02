using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerSettings", Schema = "mutual")]
    [Index("CustomerID", Name = "IDX_CustomerSettings_CustomerID_")]
    [Index("CustomerID", Name = "IDX_CustomerSettings_CustomerID_CurrentLoyaltyPoints__TotalLoyaltyPoints__IsVerified__IsConfirmed__")]
    [Index("CustomerID", "IsConfirmed", Name = "IDX_CustomerSettings_CustomerID__IsConfirmed_")]
    public partial class CustomerSetting
    {
        [Key]
        public long CustomerSettingIID { get; set; }
        public long? CustomerID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CurrentLoyaltyPoints { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalLoyaltyPoints { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsBlocked { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("CustomerSettings")]
        public virtual Customer Customer { get; set; }
    }
}
