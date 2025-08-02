using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityTypeEntitlementMap : EntityTypeConfiguration<EntityTypeEntitlement>
    {
        public EntityTypeEntitlementMap()
        {
            // Primary Key
            this.HasKey(t => t.EntitlementID);

            // Properties
            this.Property(t => t.EntitlementID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntitlementName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EntityTypeEntitlements", "mutual");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.EntitlementName).HasColumnName("EntitlementName");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");

            // Relationships
            this.HasOptional(t => t.EntityType)
                .WithMany(t => t.EntityTypeEntitlements)
                .HasForeignKey(d => d.EntityTypeID);

        }
    }
}
