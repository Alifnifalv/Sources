using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UIControlValidation
    {
        public UIControlValidation()
        {
            this.Properties = new List<Property>();
        }

        [Key]
        public byte UIControlValidationID { get; set; }
        public string ValidationName { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
