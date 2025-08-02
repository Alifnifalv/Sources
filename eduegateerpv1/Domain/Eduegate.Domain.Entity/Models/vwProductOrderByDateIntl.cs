using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductOrderByDateIntl
    {
        [Key]
        public long ProductOrderByDateIntlID { get; set; }
        public short RefCountryID { get; set; }
        public int RefOrderProductID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int OrderQuantity { get; set; }
        public int RefOrderID { get; set; }
    }
}
