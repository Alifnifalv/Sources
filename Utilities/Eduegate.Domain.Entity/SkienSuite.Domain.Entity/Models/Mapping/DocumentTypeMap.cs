using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentTypeMap : EntityTypeConfiguration<DocumentType>
    {
        public DocumentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTypeID);

            // Properties
            this.Property(t => t.DocumentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TransactionTypeName)
                .HasMaxLength(50);

            this.Property(t => t.System)
                .HasMaxLength(20);

            this.Property(t => t.TransactionNoPrefix)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocumentTypes", "mutual");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.ReferenceTypeID).HasColumnName("ReferenceTypeID");
            this.Property(t => t.TransactionTypeName).HasColumnName("TransactionTypeName");
            this.Property(t => t.System).HasColumnName("System");
            this.Property(t => t.TransactionNoPrefix).HasColumnName("TransactionNoPrefix");
            this.Property(t => t.LastTransactionNo).HasColumnName("LastTransactionNo");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.TransactionSequenceType).HasColumnName("TransactionSequenceType");

            // Relationships
            this.HasOptional(t => t.DocumentReferenceType)
                .WithMany(t => t.DocumentTypes)
                .HasForeignKey(d => d.ReferenceTypeID);

        }
    }
}
