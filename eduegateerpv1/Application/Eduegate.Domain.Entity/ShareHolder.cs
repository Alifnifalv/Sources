namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShareHolders")]
    public partial class ShareHolder
    {
        [StringLength(50)]
        public string ShareHolderID { get; set; }

        public long? LoginID { get; set; }

        public long? CustomerID { get; set; }

        [StringLength(50)]
        public string ShareHolderCode { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? TotalNumberOfShare { get; set; }

        public decimal? AmountOfInvestment { get; set; }

        [StringLength(50)]
        public string MemberShipCard { get; set; }

        public DateTime? MemberShipCardExpiry { get; set; }

        [StringLength(50)]
        public string FamilyName { get; set; }

        [StringLength(50)]
        public string NationalID { get; set; }

        public DateTime? DOB { get; set; }

        public int? Age { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public decimal? CreditLimit { get; set; }

        public decimal? Balance { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }

        public virtual Login Login { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
