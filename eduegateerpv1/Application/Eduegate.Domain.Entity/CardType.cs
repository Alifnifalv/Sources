namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.CardTypes")]
    public partial class CardType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CardType()
        {
            CustomerCards = new HashSet<CustomerCard>();
            PaymentMasterVisas = new HashSet<PaymentMasterVisa>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CardTypeID { get; set; }

        [StringLength(50)]
        public string CardName { get; set; }

        [StringLength(10)]
        public string CardCode { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCard> CustomerCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentMasterVisa> PaymentMasterVisas { get; set; }
    }
}
