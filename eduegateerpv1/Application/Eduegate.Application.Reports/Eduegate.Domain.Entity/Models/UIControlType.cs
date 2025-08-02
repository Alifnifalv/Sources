using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UIControlType
    {
        public UIControlType()
        {
            this.Properties = new List<Property>();
            this.FilterColumns = new List<FilterColumn>();
            this.ViewFilters = new List<ViewFilter>();
            this.CategorySettings = new List<CategorySetting>();
        }

        public byte UIControlTypeID { get; set; }
        public string ControlName { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
        public virtual ICollection<ViewFilter> ViewFilters { get; set; }
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }
    }
}
