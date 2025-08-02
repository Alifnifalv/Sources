using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class NotificationAlertsView
    {
        public long NotificationAlertIID { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NotificationDate { get; set; }
        public long? FromLoginID { get; set; }
        public long? ToLoginID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
    }
}
