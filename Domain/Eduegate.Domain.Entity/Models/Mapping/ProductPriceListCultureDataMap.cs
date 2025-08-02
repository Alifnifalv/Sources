using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListCultureDataMap : EntityTypeConfiguration<ProductPriceListCultureData>
    {
        public ProductPriceListCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.ProductPriceListID });

            // Properties
            this.Property(t => t.ProductPriceListID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PriceDescription)
                .HasMaxLength(255);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.PriceDescription).HasColumnName("PriceDescription");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.ProductPriceListCultureDatas)
                .HasForeignKey(d => d.CultureID);
            //this.HasRequired(t => t.ProductPriceList)
            //    .WithMany(t => t.ProductPriceListCultureDatas)
            //    .HasForeignKey(d => d.ProductPriceListID);

        }
    }
}
