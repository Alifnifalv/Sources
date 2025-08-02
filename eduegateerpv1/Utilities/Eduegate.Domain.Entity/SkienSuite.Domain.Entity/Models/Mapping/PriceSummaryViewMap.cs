using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PriceSummaryViewMap : EntityTypeConfiguration<PriceSummaryView>
    {
        public PriceSummaryViewMap()
        {
            // Primary Key
            this.HasKey(t => t.TotalActive);

            // Properties
            this.Property(t => t.TotalActive)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PriceSummaryView", "catalog");
            this.Property(t => t.TotalPriceLists).HasColumnName("TotalPriceLists");
            this.Property(t => t.TotalActive).HasColumnName("TotalActive");
        }
    }
}
