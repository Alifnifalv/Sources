using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListCustomerMapMap : EntityTypeConfiguration<ProductPriceListCustomerMap>
    {
        public ProductPriceListCustomerMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListCustomerMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListCustomerMaps", "catalog");
            this.Property(t => t.ProductPriceListCustomerMapIID).HasColumnName("ProductPriceListCustomerMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.ProductPriceListCustomerMaps)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListCustomerMaps)
                .HasForeignKey(d => d.ProductPriceListID);
            this.HasOptional(t => t.EntityTypeEntitlement)
                .WithMany(t => t.ProductPriceListCustomerMaps)
                .HasForeignKey(d => d.EntitlementID);

        }
    }
}
