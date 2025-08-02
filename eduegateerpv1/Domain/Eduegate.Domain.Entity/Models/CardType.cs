namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CardTypes", Schema = "mutual")]
    public partial class CardType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CardType()
        {
            CustomerCards = new HashSet<CustomerCard>();
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
    }
}
