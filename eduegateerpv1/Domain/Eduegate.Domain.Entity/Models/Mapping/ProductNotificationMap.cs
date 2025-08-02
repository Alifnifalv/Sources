using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductNotificationMap : EntityTypeConfiguration<ProductNotification>
    {
        public ProductNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.NotifyID);

            // Properties
            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPCountry)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductNotification");
            this.Property(t => t.NotifyID).HasColumnName("NotifyID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.NotifyOn).HasColumnName("NotifyOn");
            this.Property(t => t.NotifyEmail).HasColumnName("NotifyEmail");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
            this.Property(t => t.NotifyEmailOn).HasColumnName("NotifyEmailOn");
            this.Property(t => t.NotifyEmailBy).HasColumnName("NotifyEmailBy");
        }
    }
}
