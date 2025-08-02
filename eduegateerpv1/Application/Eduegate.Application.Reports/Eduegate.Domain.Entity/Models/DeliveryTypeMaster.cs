using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypeMaster
    {
        public DeliveryTypeMaster()
        {
            //this.CategoryMasters = new List<CategoryMaster>();
        }

        public int DeliveryTypeID { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<CategoryMaster> CategoryMasters { get; set; }
    }
}
