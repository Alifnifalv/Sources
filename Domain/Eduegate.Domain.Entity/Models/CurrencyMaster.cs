using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CurrencyMaster
    {
        public CurrencyMaster()
        {
            this.PoTrackingMasters = new List<PoTrackingMaster>();
        }

        [Key]
        public byte CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyFullName { get; set; }
        public byte NoofDecimals { get; set; }
        public virtual ICollection<PoTrackingMaster> PoTrackingMasters { get; set; }
    }
}
