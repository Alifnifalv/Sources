using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmail2015TempMap : EntityTypeConfiguration<MarketingEmail2015Temp>
    {
        public MarketingEmail2015TempMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RowID, t.CustomerID, t.FirstName, t.LastName, t.EmailID, t.Points, t.EmailSent, t.EmailSentOn });

            // Properties
            this.Property(t => t.RowID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Points)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MarketingEmail2015Temp");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.EmailSent).HasColumnName("EmailSent");
            this.Property(t => t.EmailSentOn).HasColumnName("EmailSentOn");
            this.Property(t => t.EmailOpened).HasColumnName("EmailOpened");
        }
    }
}
