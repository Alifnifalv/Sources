using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerSlabMultipriceIntl
    {
        [Key]
        public int CustomerSlabMultipriceIntlID { get; set; }
        public int RefCategoryID { get; set; }
        public int RefSlabID { get; set; }
        public decimal DiscountPer { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public decimal DiscountPerUnBranded { get; set; }
        public short RefCountryID { get; set; }
    }
}
