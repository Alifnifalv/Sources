using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentMasterLog
    {
        public int IntlPoShipmentMasterLogID { get; set; }
        public int RefIntlPoShipmentMasterID { get; set; }
        public string ShipmentStatus { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public virtual IntlPoShipmentMaster IntlPoShipmentMaster { get; set; }
    }
}
