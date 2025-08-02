using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDisableQueryBatchLogMap : EntityTypeConfiguration<ProductDisableQueryBatchLog>
    {
        public ProductDisableQueryBatchLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDisableQueryBatchLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductDisableQueryBatchLog");
            this.Property(t => t.ProductDisableQueryBatchLogID).HasColumnName("ProductDisableQueryBatchLogID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.NoOfDays).HasColumnName("NoOfDays");
            this.Property(t => t.RecordsUpdated).HasColumnName("RecordsUpdated");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
