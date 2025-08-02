using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductManagerEmailLogMap : EntityTypeConfiguration<ProductManagerEmailLog>
    {
        public ProductManagerEmailLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductManagerEmailLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductManagerEmailLog");
            this.Property(t => t.ProductManagerEmailLogID).HasColumnName("ProductManagerEmailLogID");
            this.Property(t => t.LogDate).HasColumnName("LogDate");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
        }
    }
}
