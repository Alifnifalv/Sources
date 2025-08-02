using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class NotificationSummaryView
    {
        public int? TotalNotifications { get; set; }
        public int? New { get; set; }
        public int? InProcess { get; set; }
        public int? Completed { get; set; }
        public int? Failed { get; set; }
        public int? ReProcess { get; set; }
    }
}
