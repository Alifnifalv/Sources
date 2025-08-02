using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UIControlType
    {
        public UIControlType()
        {
            this.CategorySettings = new List<CategorySetting>();
            this.Properties = new List<Property>();
            this.FilterColumns = new List<FilterColumn>();
        }

        public byte UIControlTypeID { get; set; }
        public string ControlName { get; set; }
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
    }
}
