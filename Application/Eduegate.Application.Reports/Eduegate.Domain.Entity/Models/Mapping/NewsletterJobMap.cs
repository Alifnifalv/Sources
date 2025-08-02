using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsletterJobMap : EntityTypeConfiguration<NewsletterJob>
    {
        public NewsletterJobMap()
        {
            // Primary Key
            this.HasKey(t => t.JobID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Subject)
                .HasMaxLength(50);

            this.Property(t => t.PlainText1)
                .HasMaxLength(4000);

            this.Property(t => t.PlainText2)
                .HasMaxLength(4000);

            this.Property(t => t.PlainText3)
                .HasMaxLength(4000);

            this.Property(t => t.PlainText4)
                .HasMaxLength(4000);

            this.Property(t => t.EmailIds)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("NewsletterJobs");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.TemplateID).HasColumnName("TemplateID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.PlainText1).HasColumnName("PlainText1");
            this.Property(t => t.PlainText2).HasColumnName("PlainText2");
            this.Property(t => t.PlainText3).HasColumnName("PlainText3");
            this.Property(t => t.PlainText4).HasColumnName("PlainText4");
            this.Property(t => t.EmailIds).HasColumnName("EmailIds");
            this.Property(t => t.NewsSubscribers).HasColumnName("NewsSubscribers");
            this.Property(t => t.Customers).HasColumnName("Customers");
            this.Property(t => t.createdate).HasColumnName("createdate");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CompletedOn).HasColumnName("CompletedOn");
            this.Property(t => t.SubscriberCount).HasColumnName("SubscriberCount");
            this.Property(t => t.EmailsSent).HasColumnName("EmailsSent");

            // Relationships
            this.HasRequired(t => t.NewsLetterTemplate)
                .WithMany(t => t.NewsletterJobs)
                .HasForeignKey(d => d.TemplateID);

        }
    }
}
