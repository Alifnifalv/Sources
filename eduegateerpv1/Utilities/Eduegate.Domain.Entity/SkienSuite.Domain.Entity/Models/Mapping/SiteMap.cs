using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SiteMap : EntityTypeConfiguration<Site>
    {
        public SiteMap()
        {
            // Primary Key
            this.HasKey(t => t.SiteID);

            // Properties
            this.Property(t => t.SiteID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SiteName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sites", "cms");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.SiteName).HasColumnName("SiteName");
            this.Property(t => t.MasterPageID).HasColumnName("MasterPageID");
            this.Property(t => t.HomePageID).HasColumnName("HomePageID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Page)
                .WithMany(t => t.Sites)
                .HasForeignKey(d => d.HomePageID);
            this.HasOptional(t => t.Page1)
                .WithMany(t => t.Sites1)
                .HasForeignKey(d => d.MasterPageID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.Sites)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
