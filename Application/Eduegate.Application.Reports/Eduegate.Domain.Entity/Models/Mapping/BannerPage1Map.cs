using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerPage1Map : EntityTypeConfiguration<BannerPage1>
    {
        public BannerPage1Map()
        {
            // Primary Key
            this.HasKey(t => t.BannerPagesID);

            // Properties
            this.Property(t => t.Language)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.DisplayOn)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BannerPages");
            this.Property(t => t.BannerPagesID).HasColumnName("BannerPagesID");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.DisplayID).HasColumnName("DisplayID");
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.DisplayOn).HasColumnName("DisplayOn");

            // Relationships
            this.HasRequired(t => t.BannerMaster)
                .WithMany(t => t.BannerPages)
                .HasForeignKey(d => d.BannerID);

        }
    }
}
