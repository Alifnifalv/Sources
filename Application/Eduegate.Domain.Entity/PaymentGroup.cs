namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.PaymentGroups")]
    public partial class PaymentGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentGroup()
        {
            PaymentMethods = new HashSet<PaymentMethod>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentGroupID { get; set; }

        public int? SiteID { get; set; }

        public bool? IsCustomerBlocked { get; set; }

        public bool? IsLocal { get; set; }

        public bool? IsDigitalCart { get; set; }

        public DateTime? CreatedData { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public long? UpdatedBy { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual PaymentGroup PaymentGroups1 { get; set; }

        public virtual PaymentGroup PaymentGroup1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
