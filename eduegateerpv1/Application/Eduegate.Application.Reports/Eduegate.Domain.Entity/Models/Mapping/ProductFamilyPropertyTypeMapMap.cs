using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductFamilyPropertyTypeMapMap : EntityTypeConfiguration<ProductFamilyPropertyTypeMap>
    {
        public ProductFamilyPropertyTypeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductFamilyPropertyTypeMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductFamilyPropertyTypeMaps", "catalog");
            this.Property(t => t.ProductFamilyPropertyTypeMapIID).HasColumnName("ProductFamilyPropertyTypeMapIID");
            this.Property(t => t.ProductFamilyID).HasColumnName("ProductFamilyID");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductFamily)
                .WithMany(t => t.ProductFamilyPropertyTypeMaps)
                .HasForeignKey(d => d.ProductFamilyID);

        }
    }
}
