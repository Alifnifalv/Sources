using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ViewType
    {
        public ViewType()
        {
            this.Views = new List<View>();
        }

        public byte ViewTypeID { get; set; }
        public string ViewTypeName { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
