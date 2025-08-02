using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmail2015_v1Map : EntityTypeConfiguration<MarketingEmail2015_v1>
    {
        public MarketingEmail2015_v1Map()
        {
            // Primary Key
            this.HasKey(t => t.MarketingEmailv1ID);

            // Properties
            this.Property(t => t.PmName)
                .HasMaxLength(70);

            this.Property(t => t.PmEmail)
                .HasMaxLength(70);

            this.Property(t => t.FirstName)
                .HasMaxLength(70);

            this.Property(t => t.LastName)
                .HasMaxLength(70);

            this.Property(t => t.EmailID)
                .HasMaxLength(70);

            this.Property(t => t.Telephone)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("MarketingEmail2015_v1");
            this.Property(t => t.MarketingEmailv1ID).HasColumnName("MarketingEmailv1ID");
            this.Property(t => t.PmName).HasColumnName("PmName");
            this.Property(t => t.PmMobile).HasColumnName("PmMobile");
            this.Property(t => t.PmEmail).HasColumnName("PmEmail");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.CategorizationPoints).HasColumnName("CategorizationPoints");
            this.Property(t => t.TotalLoyaltyPoints).HasColumnName("TotalLoyaltyPoints");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.EmailSent).HasColumnName("EmailSent");
        }
    }
}
