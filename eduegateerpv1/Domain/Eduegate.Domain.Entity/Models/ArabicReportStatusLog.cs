using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ArabicReportStatusLog
    {
        [Key]
        public int ArabicStatusLogID { get; set; }
        public int ActiveProducts { get; set; }
        public int ActiveProductsNoArName { get; set; }
        public int ActiveProductsNoArDesc { get; set; }
        public int ActiveProductsNoArDetails { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
