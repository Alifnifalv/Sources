using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AssetTransactionHeadMap : EntityTypeConfiguration<AssetTransactionHead>
    {
        public AssetTransactionHeadMap()
        {
            // Primary Key
            this.HasKey(t => t.HeadIID);

            // Properties
            this.Property(t => t.Remarks)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("AssetTransactionHead", "asset");
            this.Property(t => t.HeadIID).HasColumnName("HeadIID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.EntryDate).HasColumnName("EntryDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.ProcessingStatusID).HasColumnName("ProcessingStatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.DocumentReferenceStatusMap)
                .WithMany(t => t.AssetTransactionHeads)
                .HasForeignKey(d => d.DocumentStatusID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.AssetTransactionHeads)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.TransactionStatus)
                .WithMany(t => t.AssetTransactionHeads)
                .HasForeignKey(d => d.ProcessingStatusID);

        }
    }
}
