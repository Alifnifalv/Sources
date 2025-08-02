using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStockBatchPartNoLogMap : EntityTypeConfiguration<ProductStockBatchPartNoLog>
    {
        public ProductStockBatchPartNoLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductStockBatchPartNoLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductStockBatchPartNoLog");
            this.Property(t => t.ProductStockBatchPartNoLogID).HasColumnName("ProductStockBatchPartNoLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RecordsUploaded).HasColumnName("RecordsUploaded");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
