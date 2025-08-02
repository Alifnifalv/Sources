using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductItunesLog
    {
        [Key]
        public int LogID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string RequestPage { get; set; }
        public Nullable<System.DateTime> RequestOn { get; set; }
    }
}
