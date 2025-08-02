using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkVendorMaster
    {
        public BlinkVendorMaster()
        {
            this.BlinkPoOrderMasters = new List<BlinkPoOrderMaster>();
        }

        public byte BlinkVendorMasterID { get; set; }
        public string BlinkVendorMasterCode { get; set; }
        public string BlinkVendorMasterName { get; set; }
        public bool BlinkVendorMasterActive { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual ICollection<BlinkPoOrderMaster> BlinkPoOrderMasters { get; set; }
    }
}
