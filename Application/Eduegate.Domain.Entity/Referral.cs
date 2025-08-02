namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.Referrals")]
    public partial class Referral
    {
        [Key]
        public long ReferralIID { get; set; }

        public long? ReferrerCustomerID { get; set; }

        public long? ReferredCustomerID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsUsed { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Customer Customer1 { get; set; }
    }
}
