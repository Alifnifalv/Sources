using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentTypeTransactionNumberMap : EntityTypeConfiguration<DocumentTypeTransactionNumber>
    {
        public DocumentTypeTransactionNumberMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DocumentTypeID, t.Year, t.Month });

            // Properties
            this.Property(t => t.DocumentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Year)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Month)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentTypeTransactionNumbers", "mutual");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.LastTransactionNo).HasColumnName("LastTransactionNo");

            // Relationships
            this.HasRequired(t => t.DocumentType)
                .WithMany(t => t.DocumentTypeTransactionNumbers)
                .HasForeignKey(d => d.DocumentTypeID);

        }
    }
}
