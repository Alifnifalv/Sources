using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsSearchViewMap : EntityTypeConfiguration<NewsSearchView>
    {
        public NewsSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsIID);

            // Properties
            this.Property(t => t.RowCategory)
                .HasMaxLength(6);

            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.NewsIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NewsContentShort)
                .HasMaxLength(500);

            this.Property(t => t.ThumbnailUrl)
                .HasMaxLength(500);

            this.Property(t => t.NewsTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NewsSearchView", "cms");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.NewsIID).HasColumnName("NewsIID");
            this.Property(t => t.NewsContentShort).HasColumnName("NewsContentShort");
            this.Property(t => t.NewsContent).HasColumnName("NewsContent");
            this.Property(t => t.NewsTypeID).HasColumnName("NewsTypeID");
            this.Property(t => t.ThumbnailUrl).HasColumnName("ThumbnailUrl");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.NewsTypeName).HasColumnName("NewsTypeName");
        }
    }
}
