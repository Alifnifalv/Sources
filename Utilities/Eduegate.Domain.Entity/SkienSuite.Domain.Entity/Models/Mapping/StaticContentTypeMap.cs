using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class StaticContentTypeMap : EntityTypeConfiguration<StaticContentType>
    {
        public StaticContentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentTypeID);

            // Properties
            this.Property(t => t.ContentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContentTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.ContentTemplateFilePath)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("StaticContentTypes", "cms");
            this.Property(t => t.ContentTypeID).HasColumnName("ContentTypeID");
            this.Property(t => t.ContentTypeName).HasColumnName("ContentTypeName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ContentTemplateFilePath).HasColumnName("ContentTemplateFilePath");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
