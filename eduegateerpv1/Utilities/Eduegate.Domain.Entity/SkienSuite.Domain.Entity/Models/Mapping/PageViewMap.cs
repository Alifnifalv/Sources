using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PageViewMap : EntityTypeConfiguration<PageView>
    {
        public PageViewMap()
        {
            // Primary Key
            this.HasKey(t => t.PageID);

            // Properties
            this.Property(t => t.PageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SiteName)
                .HasMaxLength(50);

            this.Property(t => t.PageName)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.TemplateName)
                .HasMaxLength(50);

            this.Property(t => t.TypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PageView", "cms");
            this.Property(t => t.PageID).HasColumnName("PageID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.SiteName).HasColumnName("SiteName");
            this.Property(t => t.PageName).HasColumnName("PageName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.PageTypeID).HasColumnName("PageTypeID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.NoOfBoilerPlates).HasColumnName("NoOfBoilerPlates");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
