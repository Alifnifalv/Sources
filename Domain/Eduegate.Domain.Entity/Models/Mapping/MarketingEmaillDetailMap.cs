using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmaillDetailMap : EntityTypeConfiguration<MarketingEmaillDetail>
    {
        public MarketingEmaillDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingEmaillDetailsID);

            // Properties
            this.Property(t => t.LinkUrl)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.LinkText)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LinkImage)
                .HasMaxLength(100);

            this.Property(t => t.LinkType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ProductID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("MarketingEmaillDetails");
            this.Property(t => t.MarketingEmaillDetailsID).HasColumnName("MarketingEmaillDetailsID");
            this.Property(t => t.RefMarketingEmaillID).HasColumnName("RefMarketingEmaillID");
            this.Property(t => t.LinkUrl).HasColumnName("LinkUrl");
            this.Property(t => t.LinkText).HasColumnName("LinkText");
            this.Property(t => t.LinkImage).HasColumnName("LinkImage");
            this.Property(t => t.LinkType).HasColumnName("LinkType");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
        }
    }
}
