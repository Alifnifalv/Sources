using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class HomePageLinksVisitMap : EntityTypeConfiguration<HomePageLinksVisit>
    {
        public HomePageLinksVisitMap()
        {
            // Primary Key
            this.HasKey(t => t.HomePageLinkVisitID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .HasMaxLength(50);

            this.Property(t => t.IPCountry)
                .HasMaxLength(50);

            this.Property(t => t.VisitFrom)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("HomePageLinksVisit");
            this.Property(t => t.HomePageLinkVisitID).HasColumnName("HomePageLinkVisitID");
            this.Property(t => t.RefHomePageLinkID).HasColumnName("RefHomePageLinkID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
            this.Property(t => t.VisitFrom).HasColumnName("VisitFrom");
        }
    }
}
