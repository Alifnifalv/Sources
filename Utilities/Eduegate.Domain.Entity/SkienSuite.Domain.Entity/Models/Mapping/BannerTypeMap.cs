using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerTypeMap : EntityTypeConfiguration<BannerType>
    {
        public BannerTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerTypeID);

            // Properties
            this.Property(t => t.BannerTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BannerTypeName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BannerTypes", "cms");
            this.Property(t => t.BannerTypeID).HasColumnName("BannerTypeID");
            this.Property(t => t.BannerTypeName).HasColumnName("BannerTypeName");
        }
    }
}
