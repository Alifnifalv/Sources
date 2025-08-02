using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductWeightBatchLogMap : EntityTypeConfiguration<ProductWeightBatchLog>
    {
        public ProductWeightBatchLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductWeightBatchLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductWeightBatchLog");
            this.Property(t => t.ProductWeightBatchLogID).HasColumnName("ProductWeightBatchLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RecordsUploaded).HasColumnName("RecordsUploaded");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
