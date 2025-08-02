using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPropertyMapMap : EntityTypeConfiguration<ProductPropertyMap>
    {
        public ProductPropertyMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPropertyMapIID);

            // Properties
            this.Property(t => t.Value)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPropertyMaps", "catalog");
            this.Property(t => t.ProductPropertyMapIID).HasColumnName("ProductPropertyMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Property)
                .WithMany(t => t.ProductPropertyMaps)
                .HasForeignKey(d => d.PropertyID);
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductPropertyMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductPropertyMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.PropertyType)
                .WithMany(t => t.ProductPropertyMaps)
                .HasForeignKey(d => d.PropertyTypeID);

        }
    }
}
