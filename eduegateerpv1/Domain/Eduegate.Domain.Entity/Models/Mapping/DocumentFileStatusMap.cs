using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentFileStatusMap : EntityTypeConfiguration<DocumentFileStatus>
    {
        public DocumentFileStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentStatusID);

            // Properties
            this.Property(t => t.DocumentStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DocumentFileStatuses", "doc");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
