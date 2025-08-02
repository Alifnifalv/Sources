using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductManagerBatchLogMap : EntityTypeConfiguration<ProductManagerBatchLog>
    {
        public ProductManagerBatchLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductManagerBatchLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductManagerBatchLog");
            this.Property(t => t.ProductManagerBatchLogID).HasColumnName("ProductManagerBatchLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RecordsUploaded).HasColumnName("RecordsUploaded");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
