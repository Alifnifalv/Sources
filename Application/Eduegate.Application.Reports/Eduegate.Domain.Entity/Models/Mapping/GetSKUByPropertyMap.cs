using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class GetSKUByPropertyMap : EntityTypeConfiguration<GetSKUByProperty>
    {
        public GetSKUByPropertyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductIID, t.ProductSKUMapIID });

            // Properties
            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductName)
                .HasMaxLength(100);

            this.Property(t => t.ImageFile)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("GetSKUByProperty", "catalog");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.SKU).HasColumnName("SKU");
            this.Property(t => t.SKUPropertyTypeID).HasColumnName("SKUPropertyTypeID");
            this.Property(t => t.SKUPropertyID).HasColumnName("SKUPropertyID");
            this.Property(t => t.ImageFile).HasColumnName("ImageFile");
            this.Property(t => t.SellingQuantityLimit).HasColumnName("SellingQuantityLimit");
        }
    }
}
