using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandTagMapMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.BrandTagMap>
    {
        public BrandTagMapMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandTagMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BrandTagMaps", "catalog");
            this.Property(t => t.BrandTagMapIID).HasColumnName("BrandTagMapIID");
            this.Property(t => t.BrandTagID).HasColumnName("BrandTagID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.BrandTagMaps)
                .HasForeignKey(d => d.BrandID);
            this.HasOptional(t => t.BrandTag)
                .WithMany(t => t.BrandTagMaps)
                .HasForeignKey(d => d.BrandTagID);

        }
    }
}
