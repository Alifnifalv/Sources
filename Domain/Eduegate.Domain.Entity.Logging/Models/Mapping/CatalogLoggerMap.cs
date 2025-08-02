using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Logging.Models.Mapping
{
    public class CatalogLoggerMap : EntityTypeConfiguration<CatalogLogger>
    {
        public CatalogLoggerMap()
        {
            // Primary Key
            this.HasKey(t => t.CatalogLoggerIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CatalogLogger", "sync");
            this.Property(t => t.CatalogLoggerIID).HasColumnName("CatalogLoggerIID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
