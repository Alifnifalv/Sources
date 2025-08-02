using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DocumentReferenceTypeMap : EntityTypeConfiguration<DocumentReferenceType>
    {
        public DocumentReferenceTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ReferenceTypeID);

            // Properties
            this.Property(t => t.ReferenceTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.InventoryTypeName)
                .HasMaxLength(50);

            this.Property(t => t.System)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("DocumentReferenceTypes", "mutual");
            this.Property(t => t.ReferenceTypeID).HasColumnName("ReferenceTypeID");
            this.Property(t => t.InventoryTypeName).HasColumnName("InventoryTypeName");
            this.Property(t => t.System).HasColumnName("System");
        }
    }
}
