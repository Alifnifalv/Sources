using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStockBatchMap : EntityTypeConfiguration<ProductStockBatch>
    {
        public ProductStockBatchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductID, t.Quantity });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductStockBatch");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
        }
    }
}
