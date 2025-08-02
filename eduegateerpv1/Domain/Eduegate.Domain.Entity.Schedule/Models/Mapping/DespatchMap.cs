using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Schedule.Models.Mapping
{
    public class DespatchMap : EntityTypeConfiguration<Despatch>
    {
        public DespatchMap()
        {
            this.HasKey(t => t.DespatchIdty);

            // Table & Column Mappings
            this.ToTable("Despatches", "schedule");
            this.Property(t => t.DespatchDate).HasColumnName("DespatchDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.ParentDespatchID).HasColumnName("ParentDespatchID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.ScheduleID).HasColumnName("ScheduleID");
            this.Property(t => t.TimeFrom).HasColumnName("TimeFrom");
            this.Property(t => t.TimeTo).HasColumnName("TimeTo");
            this.Property(t => t.TotalHours).HasColumnName("TotalHours");
            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.ReceivedAmount).HasColumnName("ReceivedAmount");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TaxAmount).HasColumnName("TaxAmount");
            this.Property(t => t.TaxPercentage).HasColumnName("TaxPercentage");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.ExternalReferenceID1).HasColumnName("ExternalReferenceID1");
            this.Property(t => t.ExternalReferenceID2).HasColumnName("ExternalReferenceID2");

            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
