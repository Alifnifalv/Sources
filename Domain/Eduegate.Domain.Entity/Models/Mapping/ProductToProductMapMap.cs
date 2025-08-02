using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductToProductMapMap : EntityTypeConfiguration<ProductToProductMap>
    {
        public ProductToProductMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductToProductMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductToProductMaps", "catalog");
            this.Property(t => t.ProductToProductMapIID).HasColumnName("ProductToProductMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductIDTo).HasColumnName("ProductIDTo");
            this.Property(t => t.SalesRelationTypeID).HasColumnName("SalesRelationTypeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductToProductMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.Product1)
                .WithMany(t => t.ProductToProductMaps1)
                .HasForeignKey(d => d.ProductIDTo);

        }
    }
}
