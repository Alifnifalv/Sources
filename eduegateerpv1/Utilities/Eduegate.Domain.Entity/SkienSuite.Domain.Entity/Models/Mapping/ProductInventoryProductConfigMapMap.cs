using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductInventoryProductConfigMapMap : EntityTypeConfiguration<ProductInventoryProductConfigMap>
    {
        public ProductInventoryProductConfigMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductInventoryConfigID, t.ProductID });

            // Properties
            this.Property(t => t.ProductInventoryConfigID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductInventoryProductConfigMaps", "catalog");
            this.Property(t => t.ProductInventoryConfigID).HasColumnName("ProductInventoryConfigID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.ProductInventoryConfig)
                .WithMany(t => t.ProductInventoryProductConfigMaps)
                .HasForeignKey(d => d.ProductInventoryConfigID);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductInventoryProductConfigMaps)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
