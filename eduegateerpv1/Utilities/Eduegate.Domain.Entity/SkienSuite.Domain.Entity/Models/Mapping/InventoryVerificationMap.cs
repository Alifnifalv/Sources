using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class InventoryVerificationMap : EntityTypeConfiguration<InventoryVerification>
    {
        public InventoryVerificationMap()
        {
            // Primary Key
            this.HasKey(t => t.InventoryVerificationIID);

            // Properties
            this.Property(t => t.InventoryVerificationIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("InventoryVerifications", "inventory");
            this.Property(t => t.InventoryVerificationIID).HasColumnName("InventoryVerificationIID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.VerificationDate).HasColumnName("VerificationDate");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.StockQuantity).HasColumnName("StockQuantity");
            this.Property(t => t.VerifiedQuantity).HasColumnName("VerifiedQuantity");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.InventoryVerifications)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.InventoryVerifications)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.InventoryVerifications)
                .HasForeignKey(d => d.EmployeeID);

        }
    }
}
