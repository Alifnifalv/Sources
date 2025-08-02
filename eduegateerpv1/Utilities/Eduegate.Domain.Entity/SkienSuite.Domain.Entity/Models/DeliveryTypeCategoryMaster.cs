using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypeCategoryMaster
    {
        public int RefCategoryID { get; set; }
        public int RefDeliveryTypeID { get; set; }
        public virtual DeliveryTypeMaster DeliveryTypeMaster { get; set; }
    }
}
