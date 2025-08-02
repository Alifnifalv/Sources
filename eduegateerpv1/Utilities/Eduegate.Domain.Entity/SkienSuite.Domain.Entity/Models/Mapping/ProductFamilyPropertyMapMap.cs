using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductFamilyPropertyMapMap : EntityTypeConfiguration<ProductFamilyPropertyMap>
    {
        public ProductFamilyPropertyMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductFamilyPropertyMapIID);

            // Properties
            this.Property(t => t.Value)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductFamilyPropertyMaps", "catalog");
            this.Property(t => t.ProductFamilyPropertyMapIID).HasColumnName("ProductFamilyPropertyMapIID");
            this.Property(t => t.ProductFamilyID).HasColumnName("ProductFamilyID");
            this.Property(t => t.ProductPropertyID).HasColumnName("ProductPropertyID");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductFamily)
                .WithMany(t => t.ProductFamilyPropertyMaps)
                .HasForeignKey(d => d.ProductFamilyID);
            this.HasOptional(t => t.Property)
                .WithMany(t => t.ProductFamilyPropertyMaps)
                .HasForeignKey(d => d.ProductPropertyID);

        }
    }
}
