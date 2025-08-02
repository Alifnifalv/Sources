using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentStatusMap : EntityTypeConfiguration<DocumentStatus>
    {
        public DocumentStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentStatusID);

            // Properties
            this.Property(t => t.DocumentStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentStatuses", "mutual");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
