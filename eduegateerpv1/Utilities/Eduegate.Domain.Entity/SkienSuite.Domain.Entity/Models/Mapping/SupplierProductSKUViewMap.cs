using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierProductSKUViewMap : EntityTypeConfiguration<SupplierProductSKUView>
    {
        public SupplierProductSKUViewMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierIID);

            // Properties
            this.Property(t => t.SupplierIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SupplierProductSKUView", "catalog");
            this.Property(t => t.SupplierIID).HasColumnName("SupplierIID");
            this.Property(t => t.ProductSKUID).HasColumnName("ProductSKUID");
        }
    }
}
