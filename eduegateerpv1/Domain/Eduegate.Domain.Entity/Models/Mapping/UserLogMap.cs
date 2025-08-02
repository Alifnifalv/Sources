using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserLogMap : EntityTypeConfiguration<UserLog>
    {
        public UserLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.UserAction)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.IpLocation)
                .HasMaxLength(255);

            this.Property(t => t.IpCountry)
                .HasMaxLength(255);

            this.Property(t => t.ActionKey)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("UserLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UserAction).HasColumnName("UserAction");
            this.Property(t => t.ActionTime).HasColumnName("ActionTime");
            this.Property(t => t.IpLocation).HasColumnName("IpLocation");
            this.Property(t => t.IpCountry).HasColumnName("IpCountry");
            this.Property(t => t.ActionKey).HasColumnName("ActionKey");

            // Relationships
            this.HasRequired(t => t.UserMaster)
                .WithMany(t => t.UserLogs)
                .HasForeignKey(d => d.UserID);

        }
    }
}
