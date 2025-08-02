using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsletterSubscriptionMap : EntityTypeConfiguration<NewsletterSubscription>
    {
        public NewsletterSubscriptionMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsletterSubscriptionID);

            // Properties
            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("NewsletterSubscription", "cms");
            this.Property(t => t.NewsletterSubscriptionID).HasColumnName("NewsletterSubscriptionID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.NewsletterSubscriptions)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
