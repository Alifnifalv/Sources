using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchDocumentTypeMapMap : EntityTypeConfiguration<BranchDocumentTypeMap>
    {
        public BranchDocumentTypeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchDocumentTypeMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BranchDocumentTypeMaps", "inventory");
            this.Property(t => t.BranchDocumentTypeMapIID).HasColumnName("BranchDocumentTypeMapIID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.BranchDocumentTypeMaps)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.BranchDocumentTypeMaps)
                .HasForeignKey(d => d.DocumentTypeID);

        }
    }
}
