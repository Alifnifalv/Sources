using System;

namespace Eduegate.Domain.Entity.Schedule.Models
{
    public class Despatch
    {
        public long DespatchIdty { get; set; }
        public DateTime? DespatchDate { get; set; }
        public string Remarks { get; set; }
        public long? ParentDespatchID { get; set; }
        public long? EmployeeID { get; set; }
        public long? CustomerID { get; set; }
        public long? ScheduleID { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public float? TotalHours { get; set; }
        public decimal? Rate { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public int? StatusID { get; set; }
        public long? ExternalReferenceID1 { get; set; }
        public long? ExternalReferenceID2 { get; set; }

        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}