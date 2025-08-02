using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDeliverySummaryViewMap : EntityTypeConfiguration<ProductDeliverySummaryView>
    {
        public ProductDeliverySummaryViewMap()
        {
            // Primary Key
            this.HasKey(t => t.OutOfStocK);

            // Properties
            this.Property(t => t.LastCreated)
                .HasMaxLength(15);

            this.Property(t => t.LastUpdated)
                .HasMaxLength(15);

            this.Property(t => t.OutOfStocK)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductDeliverySummaryView", "catalog");
            this.Property(t => t.TotalProducts).HasColumnName("TotalProducts");
            this.Property(t => t.LastCreated).HasColumnName("LastCreated");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            this.Property(t => t.OutOfStocK).HasColumnName("OutOfStocK");
            this.Property(t => t.InActive).HasColumnName("InActive");
            this.Property(t => t.UnderReview).HasColumnName("UnderReview");
            this.Property(t => t.DraftMode).HasColumnName("DraftMode");
        }
    }
}
