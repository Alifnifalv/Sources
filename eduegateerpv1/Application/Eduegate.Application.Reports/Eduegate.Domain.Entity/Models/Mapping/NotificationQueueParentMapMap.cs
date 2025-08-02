using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationQueueParentMapMap : EntityTypeConfiguration<NotificationQueueParentMap>
    {
        public NotificationQueueParentMapMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationQueueParentMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("NotificationQueueParentMaps", "notification");
            this.Property(t => t.NotificationQueueParentMapIID).HasColumnName("NotificationQueueParentMapIID");
            this.Property(t => t.NotificationQueueID).HasColumnName("NotificationQueueID");
            this.Property(t => t.ParentNotificationQueueID).HasColumnName("ParentNotificationQueueID");
            this.Property(t => t.IsNotificationSuccess).HasColumnName("IsNotificationSuccess");
        }
    }
}
