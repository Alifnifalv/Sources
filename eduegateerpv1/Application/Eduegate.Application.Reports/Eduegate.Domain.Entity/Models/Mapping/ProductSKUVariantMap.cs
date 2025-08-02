using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUVariantMap : EntityTypeConfiguration<ProductSKUVariant>
    {
        public ProductSKUVariantMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductSKUMapIID, t.PropertyTypeName });

            // Properties
            //this.Property(t => t.ProductSKUMapIID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductSKUVariants", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
        }
    }
}
