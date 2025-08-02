using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentGroups", Schema = "mutual")]
    public partial class PaymentGroup
    {
        public PaymentGroup()
        {
            this.PaymentMethods = new List<PaymentMethod>();
        }

        [Key]
        public int PaymentGroupID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<bool> IsCustomerBlocked { get; set; }
        public Nullable<bool> IsLocal { get; set; }
        public Nullable<bool> IsDigitalcart { get; set; }
        public Nullable<System.DateTime> CreatedData { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public virtual Site Site { get; set; }
        //public virtual PaymentGroup PaymentGroups1 { get; set; }
        //public virtual PaymentGroup PaymentGroup1 { get; set; }
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
