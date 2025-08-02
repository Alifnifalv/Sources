using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkPoOrderMasterLog
    {
        public int BlinkPoOrderMasterLogID { get; set; }
        public int RefBlinkPoOrderMasterID { get; set; }
        public string BlinkPoOrderStatus { get; set; }
        public string Comment { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
