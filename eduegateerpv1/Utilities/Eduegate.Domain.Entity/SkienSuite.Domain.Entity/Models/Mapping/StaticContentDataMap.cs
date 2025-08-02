using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class StaticContentDataMap : EntityTypeConfiguration<StaticContentData>
    {
        public StaticContentDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ContentDataIID, t.TimeStamps });

            // Properties
            this.Property(t => t.ContentDataIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Title)
                .HasMaxLength(250);

            this.Property(t => t.ImageFilePath)
                .HasMaxLength(250);

            this.Property(t => t.TimeStamps)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("StaticContentDatas", "cms");
            this.Property(t => t.ContentDataIID).HasColumnName("ContentDataIID");
            this.Property(t => t.ContentTypeID).HasColumnName("ContentTypeID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageFilePath).HasColumnName("ImageFilePath");
            this.Property(t => t.SerializedJsonParameters).HasColumnName("SerializedJsonParameters");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.StaticContentType)
                .WithMany(t => t.StaticContentDatas)
                .HasForeignKey(d => d.ContentTypeID);

        }
    }
}
