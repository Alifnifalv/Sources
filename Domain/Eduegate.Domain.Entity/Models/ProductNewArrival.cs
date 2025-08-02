using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductNewArrival
    {
        [Key]
        public int RefNewArrivalProductID { get; set; }
        public System.DateTime ProductStartDate { get; set; }
        public System.DateTime ProductEndDate { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}
