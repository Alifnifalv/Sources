using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentTypeTypeMap : EntityTypeConfiguration<DocumentTypeType>
    {
        public DocumentTypeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTypeTypeMapIID);

            // Properties

            this.Property(t => t.DocumentTypeID);

            this.Property(t => t.DocumentTypeMapID);


            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocumentTypeTypeMaps", "mutual");
            this.Property(t => t.DocumentTypeTypeMapIID).HasColumnName("DocumentTypeTypeMapIID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentTypeMapID).HasColumnName("DocumentTypeMapID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.DocumentTypeType)
                .HasForeignKey(d => d.DocumentTypeID);

            this.HasOptional(t => t.DocumentTypeType2)
              .WithMany(t => t.DocumentTypeType2)
              .HasForeignKey(d => d.DocumentTypeMapID);

        }
    }
}
