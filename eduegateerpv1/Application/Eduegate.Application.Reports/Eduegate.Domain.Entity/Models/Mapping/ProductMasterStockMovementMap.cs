using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterStockMovementMap : EntityTypeConfiguration<ProductMasterStockMovement>
    {
        public ProductMasterStockMovementMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductMasterStockMovementID);

            // Properties
            this.Property(t => t.TransType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.RefType)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.RefValue)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ProductMasterStockMovement");
            this.Property(t => t.ProductMasterStockMovementID).HasColumnName("ProductMasterStockMovementID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.TransType).HasColumnName("TransType");
            this.Property(t => t.TransQuantity).HasColumnName("TransQuantity");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.QuantityUpdated).HasColumnName("QuantityUpdated");
            this.Property(t => t.RefType).HasColumnName("RefType");
            this.Property(t => t.RefValue).HasColumnName("RefValue");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
