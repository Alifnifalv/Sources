using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStatusBatchLogMap : EntityTypeConfiguration<ProductStatusBatchLog>
    {
        public ProductStatusBatchLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductStockBatchLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductStatusBatchLog");
            this.Property(t => t.ProductStockBatchLogID).HasColumnName("ProductStockBatchLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RecordsUploaded).HasColumnName("RecordsUploaded");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
