using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WebsiteLogMap : EntityTypeConfiguration<WebsiteLog>
    {
        public WebsiteLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.UserAction)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.IpLocation)
                .HasMaxLength(50);

            this.Property(t => t.IpCountry)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WebsiteLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.UserAction).HasColumnName("UserAction");
            this.Property(t => t.ActionTime).HasColumnName("ActionTime");
            this.Property(t => t.IpLocation).HasColumnName("IpLocation");
            this.Property(t => t.IpCountry).HasColumnName("IpCountry");
        }
    }
}
