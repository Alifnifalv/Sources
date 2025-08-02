using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class GenderMap : EntityTypeConfiguration<Gender>
    {
        public GenderMap()
        {
            // Primary Key
            this.HasKey(t => t.GenderID);

            // Properties
            this.Property(t => t.GenderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Genders", "mutual");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
