using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentReceived
    {
        public IntlPoShipmentReceived()
        {
            this.IntlPoShipmentReceivedLogs = new List<IntlPoShipmentReceivedLog>();
        }

        public int IntlPoShipmentReceivedID { get; set; }
        public int RefIntlPoShipmentDetailsID { get; set; }
        public System.DateTime ReceivedDate { get; set; }
        public string ProductPartNo { get; set; }
        public string ProductName { get; set; }
        public short QtyReceived { get; set; }
        public decimal UnitCostUSD { get; set; }
        public string ShipmentReceivedStatus { get; set; }
        public bool InvoiceDone { get; set; }
        public string Remarks { get; set; }
        public virtual IntlPoShipmentDetail IntlPoShipmentDetail { get; set; }
        public virtual ICollection<IntlPoShipmentReceivedLog> IntlPoShipmentReceivedLogs { get; set; }
    }
}
