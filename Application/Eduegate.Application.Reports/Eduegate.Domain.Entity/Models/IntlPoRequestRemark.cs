using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoRequestRemark
    {
        public int IntlPoRequestRemarkID { get; set; }
        public int RefIntlPoRequestID { get; set; }
        public string Remarks { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual IntlPoRequest IntlPoRequest { get; set; }
    }
}
