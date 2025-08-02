using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListTypeMap : EntityTypeConfiguration<ProductPriceListType>
    {
        public ProductPriceListTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListTypeID);

            // Properties
            this.Property(t => t.ProductPriceListTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PriceListTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProductPriceListTypes", "catalog");
            this.Property(t => t.ProductPriceListTypeID).HasColumnName("ProductPriceListTypeID");
            this.Property(t => t.PriceListTypeName).HasColumnName("PriceListTypeName");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
