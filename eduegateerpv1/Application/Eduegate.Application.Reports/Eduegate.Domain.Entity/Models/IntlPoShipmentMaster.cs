using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentMaster
    {
        public IntlPoShipmentMaster()
        {
            this.IntlPoShipmentBoxDetails = new List<IntlPoShipmentBoxDetail>();
            this.IntlPoShipmentMasterLogs = new List<IntlPoShipmentMasterLog>();
            this.IntlPoShipmentPaymentMasters = new List<IntlPoShipmentPaymentMaster>();
        }

        public int IntlPoShipmentMasterID { get; set; }
        public byte RefIntlPoShipperMasterID { get; set; }
        public System.DateTime ShipmentDate { get; set; }
        public byte NoOfBoxes { get; set; }
        public Nullable<decimal> ShipmentCostTotalUSD { get; set; }
        public Nullable<decimal> CustomsCostTotalUSD { get; set; }
        public Nullable<decimal> OtherCostTotalUSD { get; set; }
        public string AwbNo { get; set; }
        public string ShipmentOrgin { get; set; }
        public string ShipmentDestination { get; set; }
        public Nullable<System.DateTime> ExpectedDateOfArrival { get; set; }
        public string ShipmentStatus { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<IntlPoShipmentBoxDetail> IntlPoShipmentBoxDetails { get; set; }
        public virtual IntlPoShipperMaster IntlPoShipperMaster { get; set; }
        public virtual ICollection<IntlPoShipmentMasterLog> IntlPoShipmentMasterLogs { get; set; }
        public virtual ICollection<IntlPoShipmentPaymentMaster> IntlPoShipmentPaymentMasters { get; set; }
    }
}
