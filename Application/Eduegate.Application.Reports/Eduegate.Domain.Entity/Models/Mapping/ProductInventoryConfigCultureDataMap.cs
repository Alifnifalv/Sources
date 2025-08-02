using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductInventoryConfigCultureDataMap : EntityTypeConfiguration<ProductInventoryConfigCultureData>
    {
        public ProductInventoryConfigCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.ProductInventoryConfigID });

            // Properties
            this.Property(t => t.ProductInventoryConfigID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductInventoryConfigCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.ProductInventoryConfigID).HasColumnName("ProductInventoryConfigID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Details).HasColumnName("Details");

            // Relationships
            this.HasRequired(t => t.ProductInventoryConfig)
                .WithMany(t => t.ProductInventoryConfigCultureDatas)
                .HasForeignKey(d => d.ProductInventoryConfigID);
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.ProductInventoryConfigCultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
