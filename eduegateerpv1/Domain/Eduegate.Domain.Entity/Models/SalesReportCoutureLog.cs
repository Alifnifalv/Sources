using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SalesReportCoutureLog
    {
        [Key]
        public int LogID { get; set; }
        public System.DateTime LogDate { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<short> RefUserID { get; set; }
    }
}
