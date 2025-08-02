using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierProductSearchViewMap : EntityTypeConfiguration<SupplierProductSearchView>
    {
        public SupplierProductSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductIID);

            // Properties
            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            this.Property(t => t.ProductName)
                .HasMaxLength(1000);

            this.Property(t => t.BranchName)
                .HasMaxLength(255);

            this.Property(t => t.SupplierName)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("SupplierProductSearchView", "catalog");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.partno).HasColumnName("partno");
            this.Property(t => t.SupplierIID).HasColumnName("SupplierIID");
            this.Property(t => t.SupplierName).HasColumnName("SupplierName");
        }
    }
}
