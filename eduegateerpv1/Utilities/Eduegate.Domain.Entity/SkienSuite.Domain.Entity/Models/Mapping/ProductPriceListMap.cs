using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListMap : EntityTypeConfiguration<ProductPriceList>
    {
        public ProductPriceListMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListIID);

            // Properties
            this.Property(t => t.PriceDescription)
                .HasMaxLength(255);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceLists", "catalog");
            this.Property(t => t.ProductPriceListIID).HasColumnName("ProductPriceListIID");
            this.Property(t => t.PriceDescription).HasColumnName("PriceDescription");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.PricePercentage).HasColumnName("PricePercentage");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ProductPriceListTypeID).HasColumnName("ProductPriceListTypeID");
            this.Property(t => t.ProductPriceListLevelID).HasColumnName("ProductPriceListLevelID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.ProductPriceListLevel)
                .WithMany(t => t.ProductPriceLists)
                .HasForeignKey(d => d.ProductPriceListLevelID);
            this.HasOptional(t => t.ProductPriceListType)
                .WithMany(t => t.ProductPriceLists)
                .HasForeignKey(d => d.ProductPriceListTypeID);

        }
    }
}
