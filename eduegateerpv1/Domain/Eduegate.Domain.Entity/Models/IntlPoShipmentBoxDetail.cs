using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentBoxDetail
    {
        public IntlPoShipmentBoxDetail()
        {
            this.IntlPoShipmentDetails = new List<IntlPoShipmentDetail>();
        }

        [Key]
        public int IntlPoShipmentBoxDetailsID { get; set; }
        public int RefIntlPoShipmentMasterID { get; set; }
        public Nullable<bool> Status { get; set; }
        public virtual IntlPoShipmentMaster IntlPoShipmentMaster { get; set; }
        public virtual ICollection<IntlPoShipmentDetail> IntlPoShipmentDetails { get; set; }
    }
}
