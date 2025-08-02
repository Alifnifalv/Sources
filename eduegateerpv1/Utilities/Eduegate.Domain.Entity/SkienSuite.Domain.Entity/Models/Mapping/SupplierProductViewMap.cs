using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierProductViewMap : EntityTypeConfiguration<SupplierProductView>
    {
        public SupplierProductViewMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierIID);

            // Properties
            this.Property(t => t.SupplierIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SupplierProductView", "catalog");
            this.Property(t => t.SupplierIID).HasColumnName("SupplierIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
        }
    }
}
