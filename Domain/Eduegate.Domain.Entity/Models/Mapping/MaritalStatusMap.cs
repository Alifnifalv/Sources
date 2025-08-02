using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MaritalStatusMap : EntityTypeConfiguration<MaritalStatus>
    {
        public MaritalStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.MaritalStatusID);

            // Properties
            this.Property(t => t.MaritalStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MaritalStatuses", "mutual");
            this.Property(t => t.MaritalStatusID).HasColumnName("MaritalStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
