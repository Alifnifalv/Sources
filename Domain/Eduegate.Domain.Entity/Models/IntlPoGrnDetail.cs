using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoGrnDetail
    {
        public IntlPoGrnDetail()
        {
            this.IntlPoShipmentDetails = new List<IntlPoShipmentDetail>();
        }

        [Key]
        public int IntlPoGrnDetailsID { get; set; }
        public int RefIntlPoGrnMasterID { get; set; }
        public int RefIntlPoOrderDetailsID { get; set; }
        public short QtyReceived { get; set; }
        public Nullable<short> QtyShipped { get; set; }
        public virtual IntlPoGrnMaster IntlPoGrnMaster { get; set; }
        public virtual IntlPoOrderDetail IntlPoOrderDetail { get; set; }
        public virtual ICollection<IntlPoShipmentDetail> IntlPoShipmentDetails { get; set; }
    }
}
