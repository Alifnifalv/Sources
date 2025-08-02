using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentReceivedLog
    {
        [Key]
        public int IntlPoShipmentReceivedLogID { get; set; }
        public int RefIntlPoShipmentReceivedID { get; set; }
        public string ShipmentReceivedStatus { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public virtual IntlPoShipmentReceived IntlPoShipmentReceived { get; set; }
    }
}
