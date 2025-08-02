using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsLetterLoggedMessageMap : EntityTypeConfiguration<NewsLetterLoggedMessage>
    {
        public NewsLetterLoggedMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.LoggedMessageID);

            // Properties
            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("NewsLetterLoggedMessages");
            this.Property(t => t.LoggedMessageID).HasColumnName("LoggedMessageID");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.Message).HasColumnName("Message");

            // Relationships
            this.HasRequired(t => t.NewsletterJob)
                .WithMany(t => t.NewsLetterLoggedMessages)
                .HasForeignKey(d => d.JobID);

        }
    }
}
