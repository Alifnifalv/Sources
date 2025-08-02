using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeMasterMap : EntityTypeConfiguration<DeliveryTypeMaster>
    {
        public DeliveryTypeMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryTypeID);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DeliveryTypeMaster", "cms");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
