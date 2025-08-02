using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductLocationBatchLogMap : EntityTypeConfiguration<ProductLocationBatchLog>
    {
        public ProductLocationBatchLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductLocationBatchLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductLocationBatchLog");
            this.Property(t => t.ProductLocationBatchLogID).HasColumnName("ProductLocationBatchLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RecordsUploaded).HasColumnName("RecordsUploaded");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
