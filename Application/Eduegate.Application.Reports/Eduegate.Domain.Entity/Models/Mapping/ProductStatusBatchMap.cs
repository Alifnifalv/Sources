using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStatusBatchMap : EntityTypeConfiguration<ProductStatusBatch>
    {
        public ProductStatusBatchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductID });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductStatusBatch");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
        }
    }
}
