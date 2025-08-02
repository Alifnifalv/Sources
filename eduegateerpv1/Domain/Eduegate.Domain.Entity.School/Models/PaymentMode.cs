namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PaymentModes", Schema = "account")]
    public partial class PaymentMode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentMode()
        {
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
            FinalSettlementPaymentModeMaps = new HashSet<FinalSettlementPaymentModeMap>();
            RefundPaymentModeMaps = new HashSet<RefundPaymentModeMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentModeID { get; set; }

        [StringLength(50)]
        public string PaymentModeName { get; set; }

        public long? AccountId { get; set; }

        public int? TenderTypeID { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }

        public virtual TenderType TenderType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementPaymentModeMap> FinalSettlementPaymentModeMaps { get; set; }
      

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundPaymentModeMap> RefundPaymentModeMaps { get; set; }
    }
}
