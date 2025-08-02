using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierSearchViewMap : EntityTypeConfiguration<SupplierSearchView>
    {
        public SupplierSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierIID);

            // Properties
            this.Property(t => t.SupplierIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Supplier)
                .HasMaxLength(767);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(767);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(50);

            this.Property(t => t.AddressName)
                .HasMaxLength(399);

            this.Property(t => t.branchname)
                .HasMaxLength(255);

            this.Property(t => t.CompanyName)
                .HasMaxLength(100);

            this.Property(t => t.LoginEmailID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SupplierSearchView", "mutual");
            this.Property(t => t.SupplierIID).HasColumnName("SupplierIID");
            this.Property(t => t.Supplier).HasColumnName("Supplier");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.branchiid).HasColumnName("branchiid");
            this.Property(t => t.branchname).HasColumnName("branchname");
            this.Property(t => t.ProductManager).HasColumnName("ProductManager");
            this.Property(t => t.Entitlements).HasColumnName("Entitlements");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.LoginEmailID).HasColumnName("LoginEmailID");
            this.Property(t => t.companyID).HasColumnName("companyID");
        }
    }
}
