using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductManagerBatchMap : EntityTypeConfiguration<ProductManagerBatch>
    {
        public ProductManagerBatchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductManagerID, t.ProductID });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductManagerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductManagerBatch");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
        }
    }
}
