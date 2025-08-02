using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoRequestAction
    {
        public IntlPoRequestAction()
        {
            this.IntlPoRequestQuantityStatus = new List<IntlPoRequestQuantityStatu>();
        }

        public byte IntlPoRequestActionID { get; set; }
        public string RequestAction { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<IntlPoRequestQuantityStatu> IntlPoRequestQuantityStatus { get; set; }
    }
}
