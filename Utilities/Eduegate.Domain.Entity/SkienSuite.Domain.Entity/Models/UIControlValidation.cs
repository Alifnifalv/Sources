using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UIControlValidation
    {
        public UIControlValidation()
        {
            this.Properties = new List<Property>();
        }

        public byte UIControlValidationID { get; set; }
        public string ValidationName { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
