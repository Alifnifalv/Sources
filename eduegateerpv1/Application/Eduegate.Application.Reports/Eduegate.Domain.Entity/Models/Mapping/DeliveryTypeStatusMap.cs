using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeStatusMap : EntityTypeConfiguration<DeliveryTypeStatus>
    {
        public DeliveryTypeStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryTypeStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DeliveryTypeStatuses", "inventory");
            this.Property(t => t.DeliveryTypeStatusID).HasColumnName("DeliveryTypeStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
