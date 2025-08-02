using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionAllocationMap : EntityTypeConfiguration<TransactionAllocation>
    {
        public TransactionAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionAllocationIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TransactionAllocations", "inventory");
            this.Property(t => t.TransactionAllocationIID).HasColumnName("TransactionAllocationIID");
            this.Property(t => t.TrasactionDetailID).HasColumnName("TrasactionDetailID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.TransactionAllocation1)
                .WithOptional(t => t.TransactionAllocations1);
            this.HasOptional(t => t.TransactionDetail)
                .WithMany(t => t.TransactionAllocations)
                .HasForeignKey(d => d.TrasactionDetailID);

        }
    }
}
