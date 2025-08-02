using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypeMaster
    {
        public DeliveryTypeMaster()
        {
            this.DeliveryTypeCategoryMasters = new List<DeliveryTypeCategoryMaster>();
        }

        public int DeliveryTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DeliveryTypeCategoryMaster> DeliveryTypeCategoryMasters { get; set; }
    }
}
