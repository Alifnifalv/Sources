using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CardTypes", Schema = "mutual")]
    public partial class CardType
    {
        public CardType()
        {
            CustomerCards = new HashSet<CustomerCard>();
            PaymentMasterVisas = new HashSet<PaymentMasterVisa>();
        }

        [Key]
        public int CardTypeID { get; set; }
        [StringLength(50)]
        public string CardName { get; set; }
        [StringLength(10)]
        public string CardCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("CardType")]
        public virtual ICollection<CustomerCard> CustomerCards { get; set; }
        [InverseProperty("CardTypeNavigation")]
        public virtual ICollection<PaymentMasterVisa> PaymentMasterVisas { get; set; }
    }
}
