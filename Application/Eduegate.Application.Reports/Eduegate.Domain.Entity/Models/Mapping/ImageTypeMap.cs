using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ImageTypeMap : EntityTypeConfiguration<ImageType>
    {
        public ImageTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageTypeID);

            // Properties
            this.Property(t => t.TypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ImageTypes", "mutual");
            this.Property(t => t.ImageTypeID).HasColumnName("ImageTypeID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
        }
    }
}
