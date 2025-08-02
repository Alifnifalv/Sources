using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Referrals", Schema = "marketing")]
    public partial class Referral
    {
        [Key]
        public long ReferralIID { get; set; }
        public long? ReferrerCustomerID { get; set; }
        public long? ReferredCustomerID { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool? IsUsed { get; set; }

        [ForeignKey("ReferredCustomerID")]
        [InverseProperty("ReferralReferredCustomers")]
        public virtual Customer ReferredCustomer { get; set; }
        [ForeignKey("ReferrerCustomerID")]
        [InverseProperty("ReferralReferrerCustomers")]
        public virtual Customer ReferrerCustomer { get; set; }
    }
}
