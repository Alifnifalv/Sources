using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentFileMap : EntityTypeConfiguration<DocumentFile>
    {
        public DocumentFileMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentFileIID);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired();

            this.Property(t => t.Title)
                .HasMaxLength(200);

            this.Property(t => t.Tags)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.Version)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocumentFiles", "doc");
            this.Property(t => t.DocumentFileIID).HasColumnName("DocumentFileIID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Tags).HasColumnName("Tags");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ActualFileName).HasColumnName("ActualFileName");
            this.Property(t => t.DocFileTypeID).HasColumnName("DocFileTypeID");
            this.Property(t => t.OwnerEmployeeID).HasColumnName("OwnerEmployeeID");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.DocumentFileStatus)
                .WithMany(t => t.DocumentFiles)
                .HasForeignKey(d => d.DocumentStatusID);
            this.HasRequired(t => t.Employee)
                .WithMany(t => t.DocumentFiles)
                .HasForeignKey(d => d.OwnerEmployeeID);

        }
    }
}
