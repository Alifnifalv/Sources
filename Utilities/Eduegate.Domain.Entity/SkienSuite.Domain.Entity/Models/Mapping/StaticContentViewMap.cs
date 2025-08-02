using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class StaticContentViewMap : EntityTypeConfiguration<StaticContentView>
    {
        public StaticContentViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentDataIID);

            // Properties
            this.Property(t => t.ContentDataIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContentTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(250);

            this.Property(t => t.ImageFilePath)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("StaticContentView", "cms");
            this.Property(t => t.ContentDataIID).HasColumnName("ContentDataIID");
            this.Property(t => t.ContentTypeID).HasColumnName("ContentTypeID");
            this.Property(t => t.ContentTypeName).HasColumnName("ContentTypeName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SerializedJsonParameters).HasColumnName("SerializedJsonParameters");
            this.Property(t => t.ImageFilePath).HasColumnName("ImageFilePath");
        }
    }
}
