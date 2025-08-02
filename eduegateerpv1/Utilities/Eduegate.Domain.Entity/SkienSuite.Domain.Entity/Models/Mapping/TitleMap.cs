using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TitleMap : EntityTypeConfiguration<Title>
    {
        public TitleMap()
        {
            // Primary Key
            this.HasKey(t => t.TitleID);

            // Properties
            this.Property(t => t.TitleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TitleName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Titles", "mutual");
            this.Property(t => t.TitleID).HasColumnName("TitleID");
            this.Property(t => t.TitleName).HasColumnName("TitleName");
        }
    }
}
