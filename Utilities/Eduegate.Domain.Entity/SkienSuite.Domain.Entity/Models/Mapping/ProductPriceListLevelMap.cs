using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListLevelMap : EntityTypeConfiguration<ProductPriceListLevel>
    {
        public ProductPriceListLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListLevelID);

            // Properties
            this.Property(t => t.ProductPriceListLevelID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProductPriceListLevels", "catalog");
            this.Property(t => t.ProductPriceListLevelID).HasColumnName("ProductPriceListLevelID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
