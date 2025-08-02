using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkPoOrderMaster
    {
        public int BlinkPoOrderMasterID { get; set; }
        public short RefCountryID { get; set; }
        public byte RefBlinkVendorMasterID { get; set; }
        public System.DateTime BlinkPoOrderDate { get; set; }
        public string BlinkPoAwbNo { get; set; }
        public string BlinkPoOrderStatus { get; set; }
        public string Details { get; set; }
        public virtual BlinkVendorMaster BlinkVendorMaster { get; set; }
    }
}
