using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductInventoryMap : EntityTypeConfiguration<ProductInventory>
    {
        public ProductInventoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductSKUMapID, t.Batch, t.BranchID });

            // Properties
            this.Property(t => t.ProductSKUMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Batch)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BranchID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductInventories", "inventory");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.Batch).HasColumnName("Batch");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.CostPrice).HasColumnName("CostPrice");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");

            // Relationships
            this.HasRequired(t => t.ProductSKUMap)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
