using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsLetterJobEmailMap : EntityTypeConfiguration<NewsLetterJobEmail>
    {
        public NewsLetterJobEmailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.JobID, t.SubscriberId, t.SubscriberType });

            // Properties
            this.Property(t => t.JobID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubscriberId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SubscriberType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("NewsLetterJobEmails");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.SubscriberId).HasColumnName("SubscriberId");
            this.Property(t => t.SubscriberType).HasColumnName("SubscriberType");
            this.Property(t => t.MailSend).HasColumnName("MailSend");
            this.Property(t => t.SendTime).HasColumnName("SendTime");

            // Relationships
            this.HasRequired(t => t.NewsletterJob)
                .WithMany(t => t.NewsLetterJobEmails)
                .HasForeignKey(d => d.JobID);

        }
    }
}
