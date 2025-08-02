using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipperMaster
    {
        public IntlPoShipperMaster()
        {
            this.IntlPoShipmentMasters = new List<IntlPoShipmentMaster>();
        }

        [Key]
        public byte IntlPoShipperMasterID { get; set; }
        public string IntlPoShipperName { get; set; }
        public bool IntlPoShipperActive { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual ICollection<IntlPoShipmentMaster> IntlPoShipmentMasters { get; set; }
    }
}
