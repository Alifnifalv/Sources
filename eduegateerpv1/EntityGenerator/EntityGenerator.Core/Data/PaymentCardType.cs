using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentCardTypes", Schema = "payment")]
    public partial class PaymentCardType
    {
        public PaymentCardType()
        {
            PaymentMasterVisas = new HashSet<PaymentMasterVisa>();
        }

        [Key]
        public byte CardTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string CardType { get; set; }

        [InverseProperty("CardTypeNavigation")]
        public virtual ICollection<PaymentMasterVisa> PaymentMasterVisas { get; set; }
    }
}
