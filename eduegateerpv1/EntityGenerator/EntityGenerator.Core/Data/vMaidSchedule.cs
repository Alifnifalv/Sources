using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class vMaidSchedule
    {
        [Column(TypeName = "datetime")]
        public DateTime? DespatchDate { get; set; }
        public long DailyDespatchId { get; set; }
        public long? EmployeeId { get; set; }
        [Required]
        [StringLength(20)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }
        public long? CustomerId { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string CustomerCode { get; set; }
        [StringLength(200)]
        public string CustomerDetails { get; set; }
        [StringLength(114)]
        [Unicode(false)]
        public string ItemsToCarry { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string TimeFrom { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string TimeTo { get; set; }
        public double? TotalHours { get; set; }
        public int StatusId { get; set; }
        public double? ReceivedAmount { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public double? TaxAmount { get; set; }
    }
}
