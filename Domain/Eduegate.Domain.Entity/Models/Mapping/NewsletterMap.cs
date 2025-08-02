using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsletterMap : EntityTypeConfiguration<Newsletter>
    {
        public NewsletterMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsletterID);

            // Properties
            this.Property(t => t.EmailAddress)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Newsletter");
            this.Property(t => t.NewsletterID).HasColumnName("NewsletterID");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.SubscribedOn).HasColumnName("SubscribedOn");
            this.Property(t => t.Newsletter1).HasColumnName("Newsletter");
            this.Property(t => t.EmailSend).HasColumnName("EmailSend");
            this.Property(t => t.EmailBounced).HasColumnName("EmailBounced");
        }
    }
}
