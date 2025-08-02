using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WarehouseDocumentTypesViewMap : EntityTypeConfiguration<WarehouseDocumentTypesView>
    {
        public WarehouseDocumentTypesViewMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTypeID);

            // Properties
            this.Property(t => t.DocumentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TransactionTypeName)
                .HasMaxLength(50);

            this.Property(t => t.InventoryTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WarehouseDocumentTypesView", "mutual");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.ReferenceTypeID).HasColumnName("ReferenceTypeID");
            this.Property(t => t.TransactionTypeName).HasColumnName("TransactionTypeName");
            this.Property(t => t.InventoryTypeName).HasColumnName("InventoryTypeName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
