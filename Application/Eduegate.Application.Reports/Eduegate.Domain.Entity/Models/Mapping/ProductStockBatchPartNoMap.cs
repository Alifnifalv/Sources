using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStockBatchPartNoMap : EntityTypeConfiguration<ProductStockBatchPartNo>
    {
        public ProductStockBatchPartNoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductPartNo });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductPartNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Quantity)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ProductStockBatchPartNo");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductPartNo).HasColumnName("ProductPartNo");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
        }
    }
}
