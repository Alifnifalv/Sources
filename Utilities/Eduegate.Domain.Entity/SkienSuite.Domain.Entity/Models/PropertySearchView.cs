using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PropertySearchView
    {
        public long PropertyIID { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDescription { get; set; }
        public string DefaultValue { get; set; }
        public string PropertyTypeName { get; set; }
    }
}
