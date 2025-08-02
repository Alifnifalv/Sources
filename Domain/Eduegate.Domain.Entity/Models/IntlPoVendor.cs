using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoVendor
    {
        [Key]
        public short IntlPoVendorID { get; set; }
        public string IntlPoVendorCode { get; set; }
        public string IntlPoVendorName { get; set; }
        public bool IntlPoVendorActive { get; set; }
    }
}
