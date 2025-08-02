using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductInventorySerialMapMap : EntityTypeConfiguration<ProductInventorySerialMap>
    {
        public ProductInventorySerialMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductInventorySerialMapIID);

            // Properties
            this.Property(t => t.SerialNo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductInventorySerialMaps", "inventory");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.Batch).HasColumnName("Batch");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Used).HasColumnName("Used");
            this.Property(t => t.ProductInventorySerialMapIID).HasColumnName("ProductInventorySerialMapIID");

            // Relationships
            this.HasRequired(t => t.ProductSKUMap)
                .WithMany(t => t.ProductInventorySerialMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.ProductInventorySerialMaps)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.ProductInventorySerialMaps)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
