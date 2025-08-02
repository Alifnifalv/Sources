using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AdminTaskLog
    {
        public int TaskID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UpdateType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public Nullable<System.DateTime> UpdateOn { get; set; }
    }
}
