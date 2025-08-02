using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsletterSubscriberMap : EntityTypeConfiguration<NewsletterSubscriber>
    {
        public NewsletterSubscriberMap()
        {
            // Primary Key
            this.HasKey(t => t.SubscriberID);

            // Properties
            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Lang)
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("NewsletterSubscribers");
            this.Property(t => t.SubscriberID).HasColumnName("SubscriberID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.RegisteredOn).HasColumnName("RegisteredOn");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Lang).HasColumnName("Lang");
        }
    }
}
