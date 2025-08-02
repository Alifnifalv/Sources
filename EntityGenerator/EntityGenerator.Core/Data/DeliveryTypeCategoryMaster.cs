using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("DeliveryTypeCategoryMaster", Schema = "cms")]
    public partial class DeliveryTypeCategoryMaster
    {
        public int RefCategoryID { get; set; }
        public int RefDeliveryTypeID { get; set; }

        [ForeignKey("RefDeliveryTypeID")]
        public virtual DeliveryTypeMaster RefDeliveryType { get; set; }
    }
}
