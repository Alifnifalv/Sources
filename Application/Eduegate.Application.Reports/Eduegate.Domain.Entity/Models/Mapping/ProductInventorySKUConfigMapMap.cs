using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductInventorySKUConfigMapMap : EntityTypeConfiguration<ProductInventorySKUConfigMap>
    {
        public ProductInventorySKUConfigMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductInventoryConfigID, t.ProductSKUMapID });

            // Properties
            this.Property(t => t.ProductInventoryConfigID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductSKUMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductInventorySKUConfigMaps", "catalog");
            this.Property(t => t.ProductInventoryConfigID).HasColumnName("ProductInventoryConfigID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.ProductInventoryConfig)
                .WithMany(t => t.ProductInventorySKUConfigMaps)
                .HasForeignKey(d => d.ProductInventoryConfigID);
            this.HasRequired(t => t.ProductSKUMap)
                .WithMany(t => t.ProductInventorySKUConfigMaps)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
