using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierProductSummaryViewMap : EntityTypeConfiguration<SupplierProductSummaryView>
    {
        public SupplierProductSummaryViewMap()
        {
            // Primary Key
            this.HasKey(t => t.OutOfStocK);

            // Properties
            this.Property(t => t.OutOfStocK)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SupplierProductSummaryView", "catalog");
            this.Property(t => t.TotalProducts).HasColumnName("TotalProducts");
            this.Property(t => t.OutOfStocK).HasColumnName("OutOfStocK");
            this.Property(t => t.InActive).HasColumnName("InActive");
            this.Property(t => t.UnderReview).HasColumnName("UnderReview");
            this.Property(t => t.DraftMode).HasColumnName("DraftMode");
            this.Property(t => t.TotalActiveProducts).HasColumnName("TotalActiveProducts");
            this.Property(t => t.TotalBrands).HasColumnName("TotalBrands");
        }
    }
}
