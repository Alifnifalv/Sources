using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkPoOrderDetail
    {
        public BlinkPoOrderDetail()
        {
            this.BlinkPoGrnDetails = new List<BlinkPoGrnDetail>();
        }

        public int BlinkPoOrderDetailsID { get; set; }
        public int RefBlinkPoOrderMasterID { get; set; }
        public int RefProductID { get; set; }
        public short QtyOrder { get; set; }
        public bool Active { get; set; }
        public Nullable<short> QtyReceived { get; set; }
        public virtual ICollection<BlinkPoGrnDetail> BlinkPoGrnDetails { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}
