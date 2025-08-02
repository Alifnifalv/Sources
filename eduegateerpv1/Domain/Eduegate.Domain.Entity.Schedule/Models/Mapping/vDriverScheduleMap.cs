using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Schedule.Models.Mapping
{
    public class vDriverScheduleMap : EntityTypeConfiguration<vDriverSchedule>
    {
        public vDriverScheduleMap()
        {
            this.HasKey(t => t.ScheduleId);

            // Table & Column Mappings
            this.ToTable("vDriverSchedules", "schedule");
            this.Property(t => t.DriverScheduleIdty).HasColumnName("DriverScheduleIdty");
            this.Property(t => t.ScheduleDate).HasColumnName("ScheduleDate");
            this.Property(t => t.ScheduleId).HasColumnName("ScheduleId");
            this.Property(t => t.DriverId).HasColumnName("DriverId");
            this.Property(t => t.DriverName).HasColumnName("DriverName");
            this.Property(t => t.DriverCode).HasColumnName("DriverCode");
            this.Property(t => t.MaidId).HasColumnName("MaidId");
            this.Property(t => t.PickUp).HasColumnName("PickUp");
            this.Property(t => t.PickUp_Location).HasColumnName("PickUp_Location");
            this.Property(t => t.DropOff).HasColumnName("DropOff");
            this.Property(t => t.AreaId).HasColumnName("AreaId");
            this.Property(t => t.MaidCode).HasColumnName("MaidCode");
            this.Property(t => t.MaidName).HasColumnName("MaidName");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
        }
    }
}
