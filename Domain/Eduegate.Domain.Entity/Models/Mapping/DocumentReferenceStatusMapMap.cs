using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentReferenceStatusMapMap : EntityTypeConfiguration<DocumentReferenceStatusMap>
    {
        public DocumentReferenceStatusMapMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentReferenceStatusMapID);

            // Properties
            this.Property(t => t.DocumentReferenceStatusMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentReferenceStatusMap", "mutual");
            this.Property(t => t.DocumentReferenceStatusMapID).HasColumnName("DocumentReferenceStatusMapID");
            this.Property(t => t.ReferenceTypeID).HasColumnName("ReferenceTypeID");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");

            // Relationships
            this.HasOptional(t => t.DocumentReferenceType)
                .WithMany(t => t.DocumentReferenceStatusMaps)
                .HasForeignKey(d => d.ReferenceTypeID);
            this.HasRequired(t => t.DocumentStatus)
                .WithMany(t => t.DocumentReferenceStatusMaps)
                .HasForeignKey(d => d.DocumentStatusID);

        }
    }
}
