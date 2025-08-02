using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductBarcodeBatchLogMap : EntityTypeConfiguration<ProductBarcodeBatchLog>
    {
        public ProductBarcodeBatchLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductBarCodeBatchLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductBarcodeBatchLog");
            this.Property(t => t.ProductBarCodeBatchLogID).HasColumnName("ProductBarCodeBatchLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RecordsUploaded).HasColumnName("RecordsUploaded");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
