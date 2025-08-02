using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoRequestAction
    {
        public IntlPoRequestAction()
        {
            this.IntlPoRequestQuantityStatus = new List<IntlPoRequestQuantityStatu>();
        }

        [Key]
        public byte IntlPoRequestActionID { get; set; }
        public string RequestAction { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<IntlPoRequestQuantityStatu> IntlPoRequestQuantityStatus { get; set; }
    }
}
