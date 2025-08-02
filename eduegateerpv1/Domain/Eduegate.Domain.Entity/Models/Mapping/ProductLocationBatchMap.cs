using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductLocationBatchMap : EntityTypeConfiguration<ProductLocationBatch>
    {
        public ProductLocationBatchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductID, t.Location });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductLocationBatch");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Location).HasColumnName("Location");
        }
    }
}
