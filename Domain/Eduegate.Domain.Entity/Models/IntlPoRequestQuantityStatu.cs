using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoRequestQuantityStatu
    {
        [Key]
        public int IntlPoRequestQuantityStatusID { get; set; }
        public int RefIntlPoRequestID { get; set; }
        public byte RefIntlPoRequestActionID { get; set; }
        public short QtyRequestAction { get; set; }
        public string Details { get; set; }
        public Nullable<short> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual IntlPoRequest IntlPoRequest { get; set; }
        public virtual IntlPoRequestAction IntlPoRequestAction { get; set; }
    }
}
