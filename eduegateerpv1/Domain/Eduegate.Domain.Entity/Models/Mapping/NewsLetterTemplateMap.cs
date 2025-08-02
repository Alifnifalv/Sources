using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsLetterTemplateMap : EntityTypeConfiguration<NewsLetterTemplate>
    {
        public NewsLetterTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.TemplateID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PlainText1)
                .IsRequired()
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
            this.ToTable("NewsLetterTemplate");
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
        }
    }
}
