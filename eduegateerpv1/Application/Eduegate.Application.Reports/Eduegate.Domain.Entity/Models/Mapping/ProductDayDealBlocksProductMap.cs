using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDayDealBlocksProductMap : EntityTypeConfiguration<ProductDayDealBlocksProduct>
    {
        public ProductDayDealBlocksProductMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefBlockID, t.RefProductID, t.Position });

            // Properties
            this.Property(t => t.RefBlockID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductDayDealBlocksProducts");
            this.Property(t => t.RefBlockID).HasColumnName("RefBlockID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
        }
    }
}
