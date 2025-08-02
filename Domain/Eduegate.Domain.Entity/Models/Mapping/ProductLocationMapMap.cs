using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductLocationMapMap : EntityTypeConfiguration<ProductLocationMap>
    {
        public ProductLocationMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductLocationMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductLocationMaps", "inventory");
            this.Property(t => t.ProductLocationMapIID).HasColumnName("ProductLocationMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductLocationMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductLocationMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.Location)
                .WithMany(t => t.ProductLocationMaps)
                .HasForeignKey(d => d.LocationID);

        }
    }
}
