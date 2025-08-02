using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class HomePageLinkMap : EntityTypeConfiguration<HomePageLink>
    {
        public HomePageLinkMap()
        {
            // Primary Key
            this.HasKey(t => t.HomePageLinkID);

            // Properties
            this.Property(t => t.HomePageLinkUrl)
                .HasMaxLength(300);

            this.Property(t => t.Lang)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("HomePageLinks");
            this.Property(t => t.HomePageLinkID).HasColumnName("HomePageLinkID");
            this.Property(t => t.HomePageLinkUrl).HasColumnName("HomePageLinkUrl");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Lang).HasColumnName("Lang");
        }
    }
}
