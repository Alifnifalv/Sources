using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class StockNotificationStatusMap : EntityTypeConfiguration<StockNotificationStatus>
    {
        public StockNotificationStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.StockNotificationStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("StockNotificationStatuses", "inventory");
            this.Property(t => t.StockNotificationStatusID).HasColumnName("StockNotificationStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
