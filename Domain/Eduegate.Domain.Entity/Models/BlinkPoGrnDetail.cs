using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkPoGrnDetail
    {
        [Key]
        public int BlinkPoGrnDetailsID { get; set; }
        public int RefBlinkPoGrnMasterID { get; set; }
        public int RefBlinkPoOrderDetailsID { get; set; }
        public short QtyReceived { get; set; }
        public short QtyShipped { get; set; }
        public string BlinkPoGrnDetailsBin { get; set; }
        public virtual BlinkPoGrnMaster BlinkPoGrnMaster { get; set; }
        public virtual BlinkPoOrderDetail BlinkPoOrderDetail { get; set; }
    }
}
