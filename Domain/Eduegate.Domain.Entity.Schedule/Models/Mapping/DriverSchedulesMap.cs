using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Schedule.Models.Mapping
{
    public class DriverSchedulesMap : EntityTypeConfiguration<DriverSchedule>
    {
        public DriverSchedulesMap()
        {
            this.HasKey(t => t.DriverScheduleIdty);

            // Table & Column Mappings
            this.ToTable("DriverSchedules", "schedule");
            this.Property(t => t.DriverScheduleIdty).HasColumnName("DriverScheduleIdty");
            this.Property(t => t.ScheduleDate).HasColumnName("ScheduleDate");
            this.Property(t => t.ScheduleId).HasColumnName("ScheduleId");
            this.Property(t => t.DriverId).HasColumnName("DriverId");
            this.Property(t => t.MaidID).HasColumnName("MiadID");
            this.Property(t => t.PickUp).HasColumnName("PickUp");
            this.Property(t => t.PickupLocation).HasColumnName("PickupLocation");
            this.Property(t => t.DropOff).HasColumnName("DropOff");
            this.Property(t => t.DropOffLocation).HasColumnName("DropLocation");
            this.Property(t => t.AreaId).HasColumnName("AreaId");
            this.Property(t => t.StatusId).HasColumnName("StatusId");

            //this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            //this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            //this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
