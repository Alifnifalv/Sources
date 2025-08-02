using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmail2015Map : EntityTypeConfiguration<MarketingEmail2015>
    {
        public MarketingEmail2015Map()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(100);

            this.Property(t => t.LastName)
                .HasMaxLength(100);

            this.Property(t => t.EmailID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("MarketingEmail2015");
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
