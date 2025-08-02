using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Schedule.Models.Mapping
{
    public class vMaidScheduleMap : EntityTypeConfiguration<vMaidSchedule>
    {
        public vMaidScheduleMap()
        {
            this.HasKey(t => t.DailyDespatchId);

            // Table & Column Mappings
            this.ToTable("vMaidSchedules", "schedule");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.CustomerDetails).HasColumnName("CustomerDetails");
            this.Property(t => t.ItemsToCarry).HasColumnName("ItemsToCarry");
            this.Property(t => t.DailyDespatchId).HasColumnName("DailyDespatchId");
            this.Property(t => t.DespatchDate).HasColumnName("DespatchDate");
            this.Property(t => t.EmployeeCode).HasColumnName("EmployeeCode");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.EmployeeName).HasColumnName("EmployeeName");
            this.Property(t => t.ReceivedAmount).HasColumnName("ReceivedAmount");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.TimeFrom).HasColumnName("TimeFrom");
            this.Property(t => t.TimeTo).HasColumnName("TimeTo");
            this.Property(t => t.TotalHours).HasColumnName("TotalHours");

            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TaxAmount).HasColumnName("TaxAmount");
        }
    }
}
