using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeAllowedCountryMapMap : EntityTypeConfiguration<DeliveryTypeAllowedCountryMap>
    {
        public DeliveryTypeAllowedCountryMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DeliveryTypeID, t.FromCountryID, t.ToCountryID });

            // Properties
            this.Property(t => t.DeliveryTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FromCountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ToCountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DeliveryTypeAllowedCountryMaps", "inventory");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.FromCountryID).HasColumnName("FromCountryID");
            this.Property(t => t.ToCountryID).HasColumnName("ToCountryID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.DeliveryTypeAllowedCountryMaps)
                .HasForeignKey(d => d.FromCountryID);
            this.HasRequired(t => t.Country1)
                .WithMany(t => t.DeliveryTypeAllowedCountryMaps1)
                .HasForeignKey(d => d.ToCountryID);
            this.HasRequired(t => t.DeliveryTypes1)
                .WithMany(t => t.DeliveryTypeAllowedCountryMaps)
                .HasForeignKey(d => d.DeliveryTypeID);

        }
    }
}
