using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductWeightBatchMap : EntityTypeConfiguration<ProductWeightBatch>
    {
        public ProductWeightBatchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductID, t.ProductWeight });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductWeight)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductWeightBatch");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductWeight).HasColumnName("ProductWeight");
        }
    }
}
