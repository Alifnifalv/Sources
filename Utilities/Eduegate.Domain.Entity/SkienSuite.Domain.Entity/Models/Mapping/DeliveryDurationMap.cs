using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryDurationMap : EntityTypeConfiguration<DeliveryDuration>
    {
        public DeliveryDurationMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryDurationID);

            // Properties
            this.Property(t => t.DurationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DeliveryDuration", "inventory");
            this.Property(t => t.DeliveryDurationID).HasColumnName("DeliveryDurationID");
            this.Property(t => t.DurationName).HasColumnName("DurationName");
        }
    }
}
