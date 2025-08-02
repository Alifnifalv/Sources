using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EmailTracker
    {
        [Key]
        public int RowID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public string EmailID { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
    }
}
