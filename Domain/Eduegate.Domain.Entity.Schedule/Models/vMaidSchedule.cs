using System;

namespace Eduegate.Domain.Entity.Schedule.Models
{
    public class vMaidSchedule
    {
        public long DailyDespatchId { get; set; }
        public DateTime DespatchDate { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDetails { get; set; }
        public string ItemsToCarry { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public double TotalHours { get; set; }
        public int? StatusId { get; set; }
        public double? ReceivedAmount { get; set; }
        public double? Rate { get; set; }
        public double? Amount { get; set; }
        public double? TaxAmount { get; set; }
    }
}