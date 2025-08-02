using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterSupplierLogMap : EntityTypeConfiguration<ProductMasterSupplierLog>
    {
        public ProductMasterSupplierLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductMasterSupplierLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductMasterSupplierLog");
            this.Property(t => t.ProductMasterSupplierLogID).HasColumnName("ProductMasterSupplierLogID");
            this.Property(t => t.RefSupplierID).HasColumnName("RefSupplierID");
            this.Property(t => t.RefProductMasterID).HasColumnName("RefProductMasterID");
        }
    }
}
