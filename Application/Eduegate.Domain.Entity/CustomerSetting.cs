namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.CustomerSettings")]
    public partial class CustomerSetting
    {
        [Key]
        public long CustomerSettingIID { get; set; }

        public long? CustomerID { get; set; }

        public decimal? CurrentLoyaltyPoints { get; set; }

        public decimal? TotalLoyaltyPoints { get; set; }

        public bool? IsVerified { get; set; }

        public bool? IsConfirmed { get; set; }

        public bool? IsBlocked { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
