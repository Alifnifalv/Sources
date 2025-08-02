using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PriceSearchViewMap : EntityTypeConfiguration<PriceSearchView>
    {
        public PriceSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListIID);

            // Properties
            this.Property(t => t.PriceDescription)
                .HasMaxLength(255);

            this.Property(t => t.ProductPriceListIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PriceListTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PriceSearchView", "catalog");
            this.Property(t => t.PriceDescription).HasColumnName("PriceDescription");
            this.Property(t => t.ProductPriceListIID).HasColumnName("ProductPriceListIID");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.PricePercentage).HasColumnName("PricePercentage");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.PriceListTypeName).HasColumnName("PriceListTypeName");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
