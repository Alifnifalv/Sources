using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSkusLogMap : EntityTypeConfiguration<ProductSkusLog>
    {
        public ProductSkusLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSkusLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductSkusLog");
            this.Property(t => t.ProductSkusLogID).HasColumnName("ProductSkusLogID");
            this.Property(t => t.TotalActiveSkus).HasColumnName("TotalActiveSkus");
            this.Property(t => t.TotalSkusQuantity).HasColumnName("TotalSkusQuantity");
            this.Property(t => t.InStockSkus).HasColumnName("InStockSkus");
            this.Property(t => t.OssSkus).HasColumnName("OssSkus");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
