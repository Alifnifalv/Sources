using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmailMailGunMap : EntityTypeConfiguration<MarketingEmailMailGun>
    {
        public MarketingEmailMailGunMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingEmailMailGunID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo)
                .HasMaxLength(15);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.CampaignName)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("MarketingEmailMailGun");
            this.Property(t => t.MarketingEmailMailGunID).HasColumnName("MarketingEmailMailGunID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.CampaignName).HasColumnName("CampaignName");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.SendEmail).HasColumnName("SendEmail");
            this.Property(t => t.SendEmailOn).HasColumnName("SendEmailOn");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
