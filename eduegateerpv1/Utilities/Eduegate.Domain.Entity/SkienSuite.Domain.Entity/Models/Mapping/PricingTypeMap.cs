using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PricingTypeMap : EntityTypeConfiguration<PricingType>
    {
        public PricingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PricingTypeID);

            // Properties
            this.Property(t => t.PricingTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PricingTypes", "saloon");
            this.Property(t => t.PricingTypeID).HasColumnName("PricingTypeID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
