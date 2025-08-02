using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsMap : EntityTypeConfiguration<News>
    {
        public NewsMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsIID);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.ThumbnailUrl)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("News", "cms");
            this.Property(t => t.NewsIID).HasColumnName("NewsIID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.NewsContent).HasColumnName("NewsContent");
            this.Property(t => t.ThumbnailUrl).HasColumnName("ThumbnailUrl");
            this.Property(t => t.NewsTypeID).HasColumnName("NewsTypeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.TimeStamps).HasColumnName("CompanyID");
            this.Property(t => t.CompanyID).HasColumnName("SiteID");

            // Relationships
            this.HasOptional(t => t.NewsType)
                .WithMany(t => t.News)
                .HasForeignKey(d => d.NewsTypeID);

        }
    }
}
