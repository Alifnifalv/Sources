using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmailMap : EntityTypeConfiguration<MarketingEmail>
    {
        public MarketingEmailMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingEmaillID);

            // Properties
            this.Property(t => t.Subject)
                .HasMaxLength(150);

            this.Property(t => t.LogoLink)
                .HasMaxLength(150);

            this.Property(t => t.RefEmail)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("MarketingEmail");
            this.Property(t => t.MarketingEmaillID).HasColumnName("MarketingEmaillID");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.EmailDate).HasColumnName("EmailDate");
            this.Property(t => t.LogoLink).HasColumnName("LogoLink");
            this.Property(t => t.TextCount).HasColumnName("TextCount");
            this.Property(t => t.BannerCount).HasColumnName("BannerCount");
            this.Property(t => t.RefEmail).HasColumnName("RefEmail");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.EmailSent).HasColumnName("EmailSent");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
