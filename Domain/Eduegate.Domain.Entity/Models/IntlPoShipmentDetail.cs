using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentDetail
    {
        public IntlPoShipmentDetail()
        {
            this.IntlPoShipmentReceiveds = new List<IntlPoShipmentReceived>();
        }

        [Key]
        public int IntlPoShipmentDetailsID { get; set; }
        public int RefIntlPoGrnDetailsID { get; set; }
        public int RefIntlPoShipmentBoxDetailsID { get; set; }
        public decimal UnitCostUSD { get; set; }
        public short QtyShipped { get; set; }
        public bool Status { get; set; }
        public string MadeIn { get; set; }
        public Nullable<long> Weight { get; set; }
        public string HSCode { get; set; }
        public Nullable<short> QtyReceived { get; set; }
        public virtual IntlPoGrnDetail IntlPoGrnDetail { get; set; }
        public virtual IntlPoShipmentBoxDetail IntlPoShipmentBoxDetail { get; set; }
        public virtual ICollection<IntlPoShipmentReceived> IntlPoShipmentReceiveds { get; set; }
    }
}
