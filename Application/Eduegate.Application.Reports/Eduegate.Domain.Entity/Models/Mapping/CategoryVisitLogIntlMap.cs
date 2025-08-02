using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryVisitLogIntlMap : EntityTypeConfiguration<CategoryVisitLogIntl>
    {
        public CategoryVisitLogIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
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
            this.ToTable("CategoryVisitLogIntl");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.VisitOn).HasColumnName("VisitOn");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
        }
    }
}
