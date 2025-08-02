using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductBarcodeBatchMap : EntityTypeConfiguration<ProductBarcodeBatch>
    {
        public ProductBarcodeBatchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.ProductID });

            // Properties
            this.Property(t => t.BatchNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductBarCode)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProductBarcodeBatch");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductBarCode).HasColumnName("ProductBarCode");
            this.Property(t => t.IsDuplicateOrNot).HasColumnName("IsDuplicateOrNot");
        }
    }
}
