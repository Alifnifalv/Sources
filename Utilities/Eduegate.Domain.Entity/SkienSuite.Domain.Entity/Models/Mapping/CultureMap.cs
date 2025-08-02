using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CultureMap : EntityTypeConfiguration<Culture>
    {
        public CultureMap()
        {
            // Primary Key
            this.HasKey(t => t.CultureID);

            // Properties
            this.Property(t => t.CultureCode)
                .HasMaxLength(50);

            this.Property(t => t.CultureName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Cultures", "mutual");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.CultureCode).HasColumnName("CultureCode");
            this.Property(t => t.CultureName).HasColumnName("CultureName");
        }
    }
}
