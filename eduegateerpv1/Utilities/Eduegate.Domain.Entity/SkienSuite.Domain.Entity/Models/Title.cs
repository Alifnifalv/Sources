using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Title
    {
        public Title()
        {
            this.OrderContactMaps = new List<OrderContactMap>();
        }

        public short TitleID { get; set; }
        public string TitleName { get; set; }
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
