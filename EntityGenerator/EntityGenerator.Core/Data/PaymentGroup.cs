using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentGroups", Schema = "mutual")]
    public partial class PaymentGroup
    {
        public PaymentGroup()
        {
            PaymentMethods = new HashSet<PaymentMethod>();
        }

        [Key]
        public int PaymentGroupID { get; set; }
        public int? SiteID { get; set; }
        public bool? IsCustomerBlocked { get; set; }
        public bool? IsLocal { get; set; }
        public bool? IsDigitalCart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedData { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        [ForeignKey("PaymentGroupID")]
        [InverseProperty("PaymentGroups")]
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
